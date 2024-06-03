using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class ApplicationFlags
    {
        [JsonProperty("help")]
		public string? Help { get; set; }
        [JsonProperty("default")]
        public object? Default { get; set; }
        [JsonProperty("nargs")]
        public object? Nargs { get; set; }
        [JsonProperty("const")]
        public object? Const { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("group")]
        public object? Group { get; set; }
        [JsonProperty("argument")]
        public string? Argument { get; set; }
        [JsonProperty("metavar")]
        public object? Metavar { get; set; }
    }
}
