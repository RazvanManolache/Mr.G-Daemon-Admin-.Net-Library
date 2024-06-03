using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class ConfigData
    {
        [JsonProperty("checkDisksInterval")]
        public int CheckDisksInterval { get; set; }
        [JsonProperty("checkSubApplicationsInterval")]
        public int CheckSubApplicationsInterval { get; set; }
        [JsonProperty("checkSubApplicationsUpdateInterval")]
        public int CheckSubApplicationsUpdateInterval { get; set; }
    }
}
