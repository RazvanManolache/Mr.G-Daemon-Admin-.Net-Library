using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Data
{
    public class SubAppStatus
    {
        public string Id { get; set; }
        public string Status { get; set; }

        public bool Running { get; set; }
    }
}
