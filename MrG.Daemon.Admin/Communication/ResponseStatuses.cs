using MrG.Daemon.Control.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Reponses
{
    public class ResponseStatuses: BaseResponse
    {
        [JsonProperty("data")]
        public List<SubAppStatus> Data { get; set; }
    }
}
