﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using ErrorEventArgs = WebSocketSharp.ErrorEventArgs;

namespace SecuritySupport
{
    public class SecurityNegotiationResult
    {
        public byte[] AESKey;
        public string EstablishedTrustID;
    }

    public class SecurityNegotiationManager
    {
        //public static async Task EchoClient()
        private WebSocket Socket;
        private INegotiationProtocolMember ProtocolMember;
        private string DeviceDescription;
        Stopwatch watch = new Stopwatch();
        private bool PlayAsAlice = false;
        private SemaphoreSlim WaitingSemaphore = new SemaphoreSlim(0);
        private TimeSpan MAX_NEGOTIATION_TIME = new TimeSpan(0, 0, 0, 10);
        private string EstablishedTrustID;

        public static SecurityNegotiationResult PerformEKEInitiatorAsAlice(string connectionUrl, byte[] sharedSecret, string deviceDescription)
        {
            return performEkeInitiator(connectionUrl, sharedSecret, deviceDescription, true, null);
        }

        public static SecurityNegotiationResult PerformEKEInitiatorAsBob(string connectionUrl, byte[] sharedSecret, string deviceDescription,
            byte[] sharedSecretPayload)
        {
            return performEkeInitiator(connectionUrl, sharedSecret, deviceDescription, false, sharedSecretPayload);
        }

        private static SecurityNegotiationResult performEkeInitiator(string connectionUrl, byte[] sharedSecret, string deviceDescription,
                                                                     bool playAsAlice, byte[] sharedSecretPayload)
        {
            if (sharedSecretPayload != null && playAsAlice)
                throw new NotSupportedException("Shared secret payload is supported only on Bob - delivered at pinging remote Alice");
            var securityNegotiationManager = InitSecurityNegotiationManager(connectionUrl, sharedSecret, deviceDescription, playAsAlice);
            securityNegotiationManager.SharedSecretPayload = sharedSecretPayload;
            // Perform negotiation
            securityNegotiationManager.PerformNegotiation();
            var aesKey = securityNegotiationManager.ProtocolMember.NegotiationResults[0];
            string deviceID = securityNegotiationManager.EstablishedTrustID;
            return new SecurityNegotiationResult {AESKey = aesKey, EstablishedTrustID = deviceID};
        }

        protected byte[] SharedSecretPayload { get; set; }


        private void PerformNegotiation()
        {
            watch.Start();
            Socket.Connect();
            bool negotiationSuccess = WaitingSemaphore.Wait(MAX_NEGOTIATION_TIME);
            Socket.Close();
            if(!negotiationSuccess)
                throw new TimeoutException("Trust negotiation timed out");
        }

        public static void EchoClient()
        {
            Console.WriteLine("Starting EKE WSS connection");
            //string hostWithProtocolAndPort = "ws://localhost:50430";
            string hostWithProtocolAndPort = "wss://theball.protonit.net";
            //string idParam = "accountemail=kalle.launiala@gmail.com";
            string idParam = "groupID=4ddf4bef-0f60-41b6-925d-02721e89d637";
            string deviceConnectionUrl = hostWithProtocolAndPort + "/websocket/NegotiateDeviceConnection?" + idParam;
            //socket = new WebSocket("wss://theball.protonit.net/websocket/mytest.k");
            string sharedSecret = "testsecretXYZ33";
            var securityNegotiationManager = InitSecurityNegotiationManager(deviceConnectionUrl, Encoding.UTF8.GetBytes(sharedSecret), "test device desc", false);
            securityNegotiationManager.PerformNegotiation();
#if native45

    //WebSocket socket = new ClientWebSocket();
    //WebSocket.CreateClientWebSocket()
            ClientWebSocket socket = new ClientWebSocket();
            Uri uri = new Uri("ws://localhost:50430/websocket/mytest.k");
            var cts = new CancellationTokenSource();
            await socket.ConnectAsync(uri, cts.Token);

            Console.WriteLine(socket.State);

            Task.Factory.StartNew(
                async () =>
                {
                    var rcvBytes = new byte[128];
                    var rcvBuffer = new ArraySegment<byte>(rcvBytes);
                    while (true)
                    {
                        WebSocketReceiveResult rcvResult = await socket.ReceiveAsync(rcvBuffer, cts.Token);
                        byte[] msgBytes = rcvBuffer.Skip(rcvBuffer.Offset).Take(rcvResult.Count).ToArray();
                        string rcvMsg = Encoding.UTF8.GetString(msgBytes);
                        Console.WriteLine("Received: {0}", rcvMsg);
                    }
                }, cts.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            while (true)
            {
                var message = Console.ReadLine();
                if (message == "Bye")
                {
                    cts.Cancel();
                    return;
                }
                byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                var sendBuffer = new ArraySegment<byte>(sendBytes);
                await
                    socket.SendAsync(sendBuffer, WebSocketMessageType.Text, endOfMessage: true,
                                     cancellationToken: cts.Token);
            }

#endif
        }

        private static SecurityNegotiationManager InitSecurityNegotiationManager(string deviceConnectionUrl, byte[] sharedSecret, string deviceDescription, bool playAsAlice)
        {
            SecurityNegotiationManager securityNegotiationManager = new SecurityNegotiationManager();
            securityNegotiationManager.Socket = new WebSocket(deviceConnectionUrl);
            securityNegotiationManager.Socket.OnOpen += securityNegotiationManager.socket_OnOpen;
            securityNegotiationManager.Socket.OnClose += securityNegotiationManager.socket_OnClose;
            securityNegotiationManager.Socket.OnError += securityNegotiationManager.socket_OnError;
            securityNegotiationManager.Socket.OnMessage += securityNegotiationManager.socket_OnMessage;
            TheBallEKE instance = new TheBallEKE();
            instance.InitiateCurrentSymmetricFromSecret(sharedSecret);
            securityNegotiationManager.PlayAsAlice = playAsAlice;
            securityNegotiationManager.DeviceDescription = deviceDescription;
            if (securityNegotiationManager.PlayAsAlice)
            {
                securityNegotiationManager.ProtocolMember = new TheBallEKE.EKEAlice(instance);
            }
            else
            {
                securityNegotiationManager.ProtocolMember = new TheBallEKE.EKEBob(instance);
            }
            securityNegotiationManager.ProtocolMember.SendMessageToOtherParty =
                bytes => { securityNegotiationManager.Socket.Send(bytes); };
            return securityNegotiationManager;
        }

        void socket_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.WriteLine("Received message: " + (e.RawData != null? e.RawData.Length.ToString() : e.Data));
            if (!ProtocolMember.IsDoneWithProtocol)
            {
                ProtocolMember.LatestMessageFromOtherParty = e.RawData;
                ProceedProtocol();
            }
            else // Last message after the protocol and then close up
            {
                if(String.IsNullOrEmpty(e.Data))
                    throw new InvalidDataException("Negotiation protocol end requires EstablishedTrustID as text");
                EstablishedTrustID = e.Data;
                watch.Stop();
                WaitingSemaphore.Release();
            }
        }

        void socket_OnError(object sender, ErrorEventArgs e)
        {
            Debug.WriteLine("ERROR: " + e.Message);
        }

        void socket_OnClose(object sender, CloseEventArgs e)
        {
            Debug.WriteLine("Closed");
        }

        void socket_OnOpen(object sender, EventArgs e)
        {
            Debug.WriteLine("Opened");
            if (PlayAsAlice)
                ProceedProtocol();
            else
                PingAlice();
        }

        private void PingAlice()
        {
            Socket.Send(SharedSecretPayload);
        }

        void ProceedProtocol()
        {
            while(ProtocolMember.IsDoneWithProtocol == false && ProtocolMember.WaitForOtherParty == false)
            {
                ProtocolMember.PerformNextAction();
            } 
            if (ProtocolMember.IsDoneWithProtocol)
            {
                Socket.Send(DeviceDescription); 
                Debug.WriteLine((PlayAsAlice ? "Alice" : "Bob") + " done with EKE in " + watch.ElapsedMilliseconds.ToString() + " ms!");
            }
        }

    }
}