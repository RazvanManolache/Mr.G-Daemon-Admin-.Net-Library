using MrG.Daemon.Control.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Events
{
    public class SubApplicationEvent
    {
        public List<DiskInfo> DiskInfo { get; internal set; }
        public List<SubApplication> SubApplication { get; internal set; }
    }
}
