﻿using System;
using System.Diagnostics;
using System.Linq;
//using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SecuritySupport
{
    public class SecurityNegotiationResult
    {
        public byte[] AESKey;
    }

    public class SecurityNegotiationManager
    {
        //public static async Task EchoClient()
        private WebSocket Socket;
        private INegotiationProtocolMember ProtocolMember;
        Stopwatch watch = new Stopwatch();
        private bool PlayAsAlice = false;
        private SemaphoreSlim WaitingSemaphore = new SemaphoreSlim(0);
        private TimeSpan MAX_NEGOTIATION_TIME = new TimeSpan(0, 0, 0, 10);

        public static SecurityNegotiationResult PerformEKEInitiatorAsAlice(string connectionUrl, string sharedSecret)
        {
            return performEkeInitiator(connectionUrl, sharedSecret, true);
        }

        public static SecurityNegotiationResult PerformEKEInitiatorAsBob(string connectionUrl, string sharedSecret)
        {
            return performEkeInitiator(connectionUrl, sharedSecret, false);
        }

        private static SecurityNegotiationResult performEkeInitiator(string connectionUrl, string sharedSecret,
                                                                     bool playAsAlice)
        {
            var securityNegotiationManager = InitSecurityNegotiationManager(connectionUrl, sharedSecret, playAsAlice);
            // Perform negotiation
            securityNegotiationManager.PerformNegotiation();
            var aesKey = securityNegotiationManager.ProtocolMember.NegotiationResults[0];
            return new SecurityNegotiationResult {AESKey = aesKey};
        }


        private void PerformNegotiation()
        {
            watch.Start();
            Socket.Connect();
            bool negotiationSuccess = WaitingSemaphore.Wait(MAX_NEGOTIATION_TIME);
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
            var securityNegotiationManager = InitSecurityNegotiationManager(deviceConnectionUrl, sharedSecret, false);
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

        private static SecurityNegotiationManager InitSecurityNegotiationManager(string deviceConnectionUrl, string sharedSecret, bool playAsAlice)
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
            ProtocolMember.LatestMessageFromOtherParty = e.RawData;
            ProceedProtocol();
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
            Socket.Send(new byte[0]);
        }

        void ProceedProtocol()
        {
            while(ProtocolMember.IsDoneWithProtocol == false && ProtocolMember.WaitForOtherParty == false)
            {
                ProtocolMember.PerformNextAction();
            } 
            if (ProtocolMember.IsDoneWithProtocol)
            {
                watch.Stop();
                Debug.WriteLine((PlayAsAlice ? "Alice" : "Bob") + " done with EKE in " + watch.ElapsedMilliseconds.ToString() + " ms!");
                WaitingSemaphore.Release();
            }
        }

    }
}