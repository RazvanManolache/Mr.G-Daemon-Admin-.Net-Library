using MrG.Daemon.Control.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Reponses
{
    public class ResponseConfig:BaseResponse
    {
        [JsonProperty("data")]
        public ConfigData Data { get; set; }
    }
}
