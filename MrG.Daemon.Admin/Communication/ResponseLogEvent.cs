using MrG.Daemon.Control.Data;
using MrG.Daemon.Control.Reponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Communication
{
    public class ResponseLogEvent : BaseResponse
    {
        public LogEvent Data { get; set; }

    }
}
