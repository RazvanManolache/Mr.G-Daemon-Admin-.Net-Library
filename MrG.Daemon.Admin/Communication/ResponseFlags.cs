using MrG.Daemon.Control.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Reponses
{
    public class ResponseFlags : BaseResponse
    {
        public ApplicationFlagsData Data { get; set; }
    }
}
