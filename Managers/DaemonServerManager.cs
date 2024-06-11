using MrG.Daemon.Control.Communication;
using MrG.Daemon.Control.Data;
using MrG.Daemon.Control.Enums;
using MrG.Daemon.Control.Events;
using MrG.Daemon.Control.Reponses;
using MrG.Daemon.Control.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MrG.Daemon.Control.Managers
{
    public class DaemonServerManager
    {
        WebSocketClient SocketClient;
        public bool Started { get; private set; }
        private string ServiceName = "MrG.Daemon";
        public string Port { get; private set; } = "8180";

        public string IP { get; private set; }
        public bool IsConnected { get; private set; }

        public List<DiskInfo> DiskInfo = new List<DiskInfo>();

        public event EventHandler<List<DiskInfo>?>? DiskInfoEvent;
        public event EventHandler<ApplicationFlagsData?>? FlagsEvent;
        public event EventHandler<List<SubApplication>?>? SubApplicationsEvent;
        public event EventHandler<List<SubApplication>?>? KitsEvent;
        public event EventHandler<ConfigData?>? ConfigEvent;
        public event EventHandler<List<SubAppStatus>?>? StatusesEvent;
        public event EventHandler<LogEvent?>? LogEvent;
        public event EventHandler<LogEvent?>? ConsoleEvent;

        public event EventHandler<WebServerEvent>? WebServerEvent;

        public DaemonServerManager(RemoteServerConfig config) 
        {
            switch (config.ServerType)
            {
                case ServerType.Local:
                    IP = "127.0.0.1";
                break;
                case ServerType.Remote:
                    IP = config.IP;
                    break;
                default:
                    throw new Exception("Invalid Server Type");
            }
            SocketClient = new WebSocketClient(GetUrl("ws"));
            SocketClient.Connected += SocketClient_Connected;
            SocketClient.Disconnected += SocketClient_Disconnected;
            SocketClient.MessageReceived += SocketClient_MessageReceived;
        }

     

        private void SocketClient_MessageReceived(string message)
        {
            //decode from base64
          //  var messageText = Encoding.UTF8.GetString(Convert.FromBase64String(message));

            var response = JsonConvert.DeserializeObject<BaseResponse>(message);

            switch (response.Type.ToLower())
            {               
                case "diskinfo":
                    var diskinfo = JsonConvert.DeserializeObject<ResponseDiskInfo>(message);                    
                    DiskInfoEvent?.Invoke(this, diskinfo?.Data);
                    break;
                case "flags":
                    var flags = JsonConvert.DeserializeObject<ResponseFlags>(message);
                    FlagsEvent?.Invoke(this, flags?.Data);
                    break;
                case "kits":
                    var kits = JsonConvert.DeserializeObject<ResponseSubApplications>(message);
                    KitsEvent?.Invoke(this, kits?.Data);
                    break;
                case "subapplications":
                    var subapplications = JsonConvert.DeserializeObject<ResponseSubApplications>(message);
                    SubApplicationsEvent?.Invoke(this, subapplications?.Data);
                    break;
                case "config":
                    var config = JsonConvert.DeserializeObject<ResponseConfig>(message);
                    ConfigEvent?.Invoke(this, config?.Data);
                    break;
                case "statuses":
                    var statuses = JsonConvert.DeserializeObject<ResponseStatuses>(message);
                    StatusesEvent?.Invoke(this, statuses?.Data);
                    break;
                case "log":
                    var log = JsonConvert.DeserializeObject<ResponseLogEvent>(message);
                    LogEvent?.Invoke(this, log?.Data);
                    break;
                case "console":
                    var console = JsonConvert.DeserializeObject<ResponseLogEvent>(message);
                    ConsoleEvent?.Invoke(this, console?.Data);
                    break;
                default:
                    break;
            }
        }

        public void SendMessage(BaseRequest request)
        {
            SocketClient.SendMessageAsync(JsonConvert.SerializeObject(request));
        }



        private void SocketClient_Disconnected()
        {
            IsConnected = false;
            WebServerEvent?.Invoke(this, new WebServerEvent { Connected = false });
        }

        private void SocketClient_Connected()
        {
            IsConnected = true;
            WebServerEvent?.Invoke(this, new WebServerEvent { Connected = true });
        }

        public async void Start()
        {
            await SocketClient.StartAsync();
        }

        public async void Stop()
        {
            await SocketClient.StopAsync();
        }

     

  

        private string GetUrl(string endpoint)
        {
            return $"http://{IP}:{Port}/{endpoint}";
        }

        public bool Test()
        {
            HttpClient client = new HttpClient();
            try
            {
                var response = client.GetAsync(GetUrl("status")).Result;
                if (response.IsSuccessStatusCode && response.Content.ReadAsStringAsync().Result == ServiceName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
               
            }
            return false;
        }

        internal bool Connected()
        {
            IsConnected = true;
            WebServerEvent?.Invoke(this, new WebServerEvent { Connected = true });
            return true;
        }


    }
}
