using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StoplightController.API
{
    public class WebSocketConnection
    {
        private WebSocket _webSocket;

        public Guid Id => Guid.NewGuid();

        public WebSocketCloseStatus? CloseStatus { get; private set; } = null;

        public string CloseStatusDescription { get; private set; } = null;

        public event EventHandler<string> ReceiveText;

        public event EventHandler<byte[]> ReceiveBinary;

        private int _receivePayloadBufferSize = 4 * 1024;

        public WebSocketConnection(WebSocket webSocket)
        {
            _webSocket = webSocket ?? throw new ArgumentNullException(nameof(webSocket));
        }

        public async Task SendAsync(string message, CancellationToken cancellationToken)
        {
            
        }

        public async Task SendAsync(byte[] message, CancellationToken cancellationToken)
        {
            
        }

        public async Task ReceiveMessagesUntilCloseAsync()
        {
            byte[] receivePayloadBuffer = new byte[_receivePayloadBufferSize];
            WebSocketReceiveResult webSocketReceiveResult =
                await _webSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer),
                    CancellationToken.None);

            while (webSocketReceiveResult.MessageType != WebSocketMessageType.Close)
            {
                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Binary)
                {
                    byte[] webSocketMessage = await ReceiveMessagePayloadAsync(_webSocket, webSocketReceiveResult,
                        receivePayloadBuffer);
                    ReceiveBinary?.Invoke(this, webSocketMessage);
                }
                else
                {
                    byte[] webSocketMessage = await ReceiveMessagePayloadAsync(_webSocket, webSocketReceiveResult,
                        receivePayloadBuffer);
                    ReceiveText?.Invoke(this, Encoding.UTF8.GetString(webSocketMessage));
                }

                webSocketReceiveResult =
                    await _webSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer),
                        CancellationToken.None);
            }

            CloseStatus = webSocketReceiveResult.CloseStatus.Value;
            CloseStatusDescription = webSocketReceiveResult.CloseStatusDescription;
        }

        private static async Task<byte[]> ReceiveMessagePayloadAsync(WebSocket webSocket,
            WebSocketReceiveResult webSocketReceiveResult, byte[] receivePayloadBuffer)
        {
            byte[] messagePayload = null;

            if (webSocketReceiveResult.EndOfMessage)
            {
                messagePayload = new byte[webSocketReceiveResult.Count];
                Array.Copy(receivePayloadBuffer, messagePayload, webSocketReceiveResult.Count);
            }
            else
            {
                using (MemoryStream messagePayloadStream = new MemoryStream())
                {
                    messagePayloadStream.Write(receivePayloadBuffer, 0, webSocketReceiveResult.Count);
                    while (!webSocketReceiveResult.EndOfMessage)
                    {
                        webSocketReceiveResult =
                            await webSocket.ReceiveAsync(new ArraySegment<byte>(receivePayloadBuffer),
                                CancellationToken.None);
                        messagePayloadStream.Write(receivePayloadBuffer, 0, webSocketReceiveResult.Count);
                    }

                    messagePayload = messagePayloadStream.ToArray();
                }
            }

            return messagePayload;
        }
    }
}