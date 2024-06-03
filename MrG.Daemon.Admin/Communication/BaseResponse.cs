using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Reponses
{
    public class BaseResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
