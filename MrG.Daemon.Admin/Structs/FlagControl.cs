using MrG.Daemon.Control.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Structs
{
    public class FlagControl
    {
        [JsonPropertyName("flag")]
        public string Flag { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("operation")]
        public FlagOperation Operation { get; set; }

        public FlagControl(string flag, string value, FlagOperation operation)
        {
            Flag = flag;
            Value = value;
            Operation = operation;
        }
    }
}
