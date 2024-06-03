using MrG.Daemon.Control.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Structs
{
    internal struct ApplicationControl
    {

        [JsonPropertyName("application")]
        internal string Application { get; set; }

        [JsonPropertyName("action")]
        internal SubApplicationAction Action { get; set; }

        [JsonPropertyName("flagOptions")]
        public List<FlagControl> FlagOptions { get; set; }

        public ApplicationControl(string application, SubApplicationAction action, List<FlagControl> flagOptions)
        {
            Application = application;
            Action = action;
            FlagOptions = flagOptions;
        }

    }
}
