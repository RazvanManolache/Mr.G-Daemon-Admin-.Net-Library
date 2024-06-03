using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{

    public class ApplicationFlagsData
    {
        public Dictionary<string, ApplicationFlags>? Flags { get; set; }
        public Dictionary<string, ApplicationFlagGroups>? Groups { get; set; }
    }
}
