using MrG.Daemon.Control.Data;
using MrG.Daemon.Control.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Communication
{

    public class BaseRequest
    {
        [JsonProperty("request")]
        public RequestTypeEnum Request { get; set; }
        [JsonProperty("requestId")]
        public string RequestId { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("app")]
        public SubApplication? App { get; set; }
        [JsonProperty("config")]
        public Dictionary<string, string>? Config { get; set; }
    }
}
