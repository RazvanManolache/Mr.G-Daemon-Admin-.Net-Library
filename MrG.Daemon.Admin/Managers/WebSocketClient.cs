using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Managers
{
    public class WebSocketClient
    {
        private ClientWebSocket _webSocket;
        private readonly Uri _uri;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private readonly TimeSpan _reconnectDelay = TimeSpan.FromSeconds(5);

        public event Action Connected;
        public event Action Disconnected;
        public event Action<string> MessageReceived;

        public WebSocketClient(string uri)
        {
            uri = uri.Replace("http://", "ws://").Replace("https://", "wss://");
            _uri = new Uri(uri);
        }

        public async Task StartAsync()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await ConnectAsync();
                    await ReceiveMessagesAsync();
                }
                catch (Exception ex)
                {
                    Disconnected?.Invoke();
                    Console.WriteLine($"Connection error: {ex.Message}");
                    await Task.Delay(_reconnectDelay);
                }
            }
        }

        public async Task StopAsync()
        {
            _cancellationTokenSource.Cancel();
            if (_webSocket != null)
            {
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client shutting down", CancellationToken.None);
                _webSocket.Dispose();
            }
        }

        private async Task ConnectAsync()
        {
            _webSocket = new ClientWebSocket();
            await _webSocket.ConnectAsync(_uri, CancellationToken.None);
            Connected?.Invoke();
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[1024 * 4];
            var message = "";
            while (_webSocket.State == WebSocketState.Open)
            {
                var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    Disconnected?.Invoke();
                }
                else
                {
                    
                    message += Encoding.UTF8.GetString(buffer, 0, result.Count);
                    if (result.EndOfMessage)
                    {
                        MessageReceived?.Invoke(message);
                        message = "";
                    }
                    
                }
            }
        }

        public async void SendMessageAsync(string message)
        {
            if (_webSocket.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(message);
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
