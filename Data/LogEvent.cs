using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class LogEvent
    {
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("appId")]
        public string? AppId { get; set; }
    }
}
