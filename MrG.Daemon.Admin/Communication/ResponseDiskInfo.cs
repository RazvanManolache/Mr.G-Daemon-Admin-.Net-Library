using MrG.Daemon.Control.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Reponses
{
    public class ResponseDiskInfo : BaseResponse
    {
        [JsonProperty("data")]
        public List<DiskInfo> Data { get; set; }
    }
}
