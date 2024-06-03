using GalaSoft.MvvmLight;
using MrG.Daemon.Control.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class RemoteServerConfig : ViewModelBase
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
                this.Set(ref _serverType, value);
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
                this.Set(ref ip, value);
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
                this.Set(ref token, value);
            }
        }


    }
}
