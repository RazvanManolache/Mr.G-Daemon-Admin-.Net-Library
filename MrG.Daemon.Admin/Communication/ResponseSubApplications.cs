using MrG.Daemon.Control.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Reponses
{
    public class ResponseSubApplications : BaseResponse
    {
        public List<SubApplication> Data { get; set; }
    }
}
