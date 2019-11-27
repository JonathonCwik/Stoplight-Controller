using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StoplightController.API.Models;

namespace StoplightController.API.Logic
{
    public class LightStateWebsocketObserver : IObserver<LightState>
    {
        private readonly WebSocket _webSocket;

        public LightStateWebsocketObserver(WebSocket webSocket)
        {
            _webSocket = webSocket;
        }

        public void OnCompleted()
        {
            Task.Run(async () =>
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done", CancellationToken.None);
            }).Wait();
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(LightState value)
        {
            Task.Run(async () =>
            {
                var jsonString = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings() { ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });

                if (_webSocket.State == WebSocketState.Open)
                {
                    await _webSocket.SendAsync(new ArraySegment<byte>(UTF8Encoding.UTF8.GetBytes(jsonString)),
                        WebSocketMessageType.Text,
                        true, CancellationToken.None);
                }
            }).Wait();
        }
    }
}