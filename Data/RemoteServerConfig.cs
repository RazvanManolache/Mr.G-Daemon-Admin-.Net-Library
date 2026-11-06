using CommunityToolkit.Mvvm.ComponentModel;
using MrG.Daemon.Control.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class RemoteServerConfig : ObservableObject
    {
        private ServerType _serverType = ServerType.Local;
        public ServerType ServerType
        {
            get
            {
                return _serverType;
            }
            set
            {
                this.SetProperty(ref _serverType, value);
            }
        }
        private string ip = "";
        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                this.SetProperty(ref ip, value);
            }
        }

        private string token = "";
        public string Token
        {
            get
            {
                return token;
            }
            set
            {
                this.SetProperty(ref token, value);
            }
        }


    }
}
