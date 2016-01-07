﻿using System;
using System.IO;
//using System.Net.WebSockets;
using System.Linq;
using System.Net.WebSockets;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using AaltoGlobalImpact.OIP;
using AzureSupport;
using DotNetOpenAuth.OpenId.RelyingParty;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Blob;
using SecuritySupport;
using TheBall;
using TheBall.CORE;

namespace WebInterface
{
    public class WebSocketHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            bool isSocket = false;
            WebSupport.InitializeContextStorage(context.Request);
            if (context.IsWebSocketRequest)
            {
                isSocket = true;
            }
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(HandleWebSocket);
            else
                HandleNonWebsocketRequest(context);


            /*
            WebSupport.InitializeContextStorage(context.Request);
            try
            {
                if (request.Path.StartsWith(AuthEmailValidation))
                {
                    HandleEmailValidation(context);
                }        
            } finally
            {
                InformationContext.ProcessAndClearCurrent();
            }*/
        }

        private void HandleNonWebsocketRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;
            if (request.Path == "/websocket/RequestSharedSecret" && request.HttpMethod == "POST")
            {
                DateTime validUntil = DateTime.UtcNow.AddSeconds(30);
                var rndGen = RandomNumberGenerator.Create();
                var sharedSecret = new byte[32];
                var nonse = new byte[32];
                var ticksValue = validUntil.Ticks;
                var ticksBytes = BitConverter.GetBytes(ticksValue);
                rndGen.GetBytes(sharedSecret);
                rndGen.GetNonZeroBytes(nonse);
                var combinedToEncrypt = nonse.Concat(sharedSecret).Concat(ticksBytes).ToArray();
                var plaintext = sharedSecret;
                var encrypted = EncryptionSupport.EncryptData(combinedToEncrypt);
                response.BinaryWrite(plaintext);
                response.BinaryWrite(encrypted);
                response.StatusCode = 200;
            }
            else
                context.Response.StatusCode = 400;
        }

        private async Task HandleWebSocket(WebSocketContext wsContext)
        {
            const int maxMessageSize = 16 * 1024;
            byte[] receiveBuffer = new byte[maxMessageSize];
            WebSocket socket = wsContext.WebSocket;

            Func<InformationContext, WebSocketContext, WebSocket, byte[], string, Task> onReceiveMessage = HandleDeviceNegotiations;
            Action<WebSocketContext, WebSocket> OnClose = HandleCloseMessage;
            //InformationContext informationContext = new InformationContext();
            InformationContext informationContext = InformationContext.Current;
            var request = HttpContext.Current.Request;
            string accountEmail = request.Params["accountemail"];
            string groupID = request.Params["groupID"];
            IContainerOwner executionOwner = null;
            if (String.IsNullOrEmpty(accountEmail) == false)
            {
                string emailRootID = TBREmailRoot.GetIDFromEmailAddress(accountEmail);
                var emailRoot = ObjectStorage.RetrieveFromDefaultLocation<TBREmailRoot>(emailRootID);
                if(emailRoot == null)
                    throw new SecurityException("No such email defined: " + accountEmail);
                executionOwner = emailRoot.Account;
            } else if (String.IsNullOrEmpty(groupID) == false)
            {
                TBRGroupRoot groupRoot = ObjectStorage.RetrieveFromDefaultLocation<TBRGroupRoot>(groupID);
                if(groupRoot == null)
                    throw new SecurityException("No such groupID defined: " + groupID);
                executionOwner = groupRoot.Group;
            }

            await InformationContext.ExecuteAsOwnerAsync(executionOwner, async () =>
            {
                while (socket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult receiveResult =
                        await socket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
                    if (receiveResult.MessageType == WebSocketMessageType.Close)
                    {
                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                        OnClose(wsContext, socket);
                    }
                    else if (receiveResult.MessageType == WebSocketMessageType.Binary)
                    {
                        //await
                        //    socket.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "Cannot accept binary frame",
                        //                      CancellationToken.None);

                        int count = receiveResult.Count;

                        while (receiveResult.EndOfMessage == false)
                        {
                            if (count >= maxMessageSize)
                            {
                                string closeMessage = string.Format("Maximum message size: {0} bytes.", maxMessageSize);
                                await
                                    socket.CloseAsync(WebSocketCloseStatus.MessageTooBig, closeMessage,
                                                      CancellationToken.None);
                                return;
                            }
                            receiveResult =
                                await
                                socket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer, count, maxMessageSize - count),
                                                    CancellationToken.None);
                            count += receiveResult.Count;
                        }
                        //var receivedString = Encoding.UTF8.GetString(receiveBuffer, 0, count);
                        byte[] binaryMessage = new byte[count];
                        Array.Copy(receiveBuffer, binaryMessage, count);
                        await onReceiveMessage(informationContext, wsContext, socket, binaryMessage, null);
                    }
                    else
                    {
                        int count = receiveResult.Count;

                        while (receiveResult.EndOfMessage == false)
                        {
                            if (count >= maxMessageSize)
                            {
                                string closeMessage = string.Format("Maximum message size: {0} bytes.", maxMessageSize);
                                await
                                    socket.CloseAsync(WebSocketCloseStatus.MessageTooBig, closeMessage,
                                                      CancellationToken.None);
                                return;
                            }
                            receiveResult =
                                await
                                socket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer, count, maxMessageSize - count),
                                                    CancellationToken.None);
                            count += receiveResult.Count;
                        }
                        var textMessage = Encoding.UTF8.GetString(receiveBuffer, 0, count);
                        await onReceiveMessage(informationContext, wsContext, socket, null, textMessage);
                    }
                }

            });

        }

        private static void HandleCloseMessage(WebSocketContext wsCtx, WebSocket socket)
        {
        }

        private async static Task HandleDeviceNegotiations(InformationContext iCtx, WebSocketContext wsCtx, WebSocket socket, byte[] binaryMessage, string textMessage)
        {
            if(iCtx != InformationContext.Current)
                throw new InvalidDataException("InformationContext is mismatched during processing");
            bool playBob = false;
            INegotiationProtocolMember protocolParty = null;
            if (binaryMessage != null)
            {
                iCtx.AccessLockedItems(dict =>
                {
                    if (dict.ContainsKey("EKENEGOTIATIONPARTY") == false)
                    {
                        // Yes, the shared secret is fixed due to demo. We're fixing it to be separately requested or given by user... :-)
                        TheBallEKE protocolInstance = new TheBallEKE();
                        var decryptedSharedSecretPayload = EncryptionSupport.DecryptData(binaryMessage);
                        var nonse = decryptedSharedSecretPayload.Take(32).ToArray();
                        var secret = decryptedSharedSecretPayload.Skip(32).Take(32).ToArray();
                        long ticks = BitConverter.ToInt64(decryptedSharedSecretPayload, 64);
                        DateTime validUntilUtc = new DateTime(ticks, DateTimeKind.Utc);
                        if(DateTime.UtcNow > validUntilUtc)
                            throw new SecurityException("Shared secret payload is expired");
                        protocolInstance.InitiateCurrentSymmetricFromSecret(secret);
                        if(playBob)
                            protocolParty = new TheBallEKE.EKEBob(protocolInstance, true);
                        else
                            protocolParty = new TheBallEKE.EKEAlice(protocolInstance, true);
                        dict.Add("EKENEGOTIATIONPARTY", protocolParty);
                    }
                    else
                    {
                        protocolParty = (INegotiationProtocolMember)dict["EKENEGOTIATIONPARTY"];
                    }
                });
                if (protocolParty.SendMessageToOtherPartyAsync == null)
                {
                    protocolParty.SendMessageToOtherPartyAsync = async bytes => { await SendBinaryMessage(socket, bytes); };
                    if(playBob) // if we play bob we put the current message already to the pipeline
                        protocolParty.LatestMessageFromOtherParty = binaryMessage;
                }
                else
                {
                    // Alice plays first, so only after the second message we start putting messages from Bob
                    protocolParty.LatestMessageFromOtherParty = binaryMessage;
                }
                while (protocolParty.IsDoneWithProtocol == false && protocolParty.WaitForOtherParty == false)
                {
                    await protocolParty.PerformNextActionAsync();
                }

            }
            else
            {
                iCtx.AccessLockedItems(dict =>
                    {
                        if (dict.ContainsKey("EKENEGOTIATIONPARTY"))
                            protocolParty = (INegotiationProtocolMember) dict["EKENEGOTIATIONPARTY"];
                    });
                if (protocolParty != null && protocolParty.IsDoneWithProtocol) // Perform POST EKE operations
                {
                    iCtx.AccessLockedItems(dict => dict.Remove("EKENEGOTIATIONPARTY"));
                    string deviceID = FinishDeviceNegotiation(iCtx, protocolParty, textMessage);
                    await SendTextMessage(socket, deviceID);
                }
                //await SendTextMessage(socket, echoString);
            }
        }

        private static string FinishDeviceNegotiation(InformationContext iCtx, INegotiationProtocolMember protocolParty, string remainingDetails)
        {
            try
            {
                var result = CreateDeviceMembership.Execute(new CreateDeviceMembershipParameters
                    {
                        Owner = iCtx.Owner,
                        ActiveSymmetricAESKey = protocolParty.NegotiationResults[0],
                        DeviceDescription = remainingDetails
                    });
                CreateAndSendEmailValidationForDeviceJoinConfirmation.Execute(new CreateAndSendEmailValidationForDeviceJoinConfirmationParameters
                    {
                        DeviceMembership = result.DeviceMembership,
                        OwningAccount = iCtx.Owner as TBAccount,
                        OwningGroup = iCtx.Owner as TBCollaboratingGroup,
                    });
                return result.DeviceMembership.ID;
            }
            finally
            {
                iCtx.PerformFinalizingActions();
            }
        }

        private async static Task SendTextMessage(WebSocket socket, string textMessage)
        {
            ArraySegment<byte> outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(textMessage));
            await socket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async static Task SendBinaryMessage(WebSocket socket, byte[] binaryMessage)
        {
            ArraySegment<byte> outputBuffer = new ArraySegment<byte>(binaryMessage);
            await socket.SendAsync(outputBuffer, WebSocketMessageType.Binary, true, CancellationToken.None);
        }

        #endregion
    }
}
