using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestTypeEnum
    {
        //to stop service
        StopService,
        //to restart service
        RestartService,
        //configure the service
        Config,
        //get disk info
        DiskInfo,
        //get flags available for app
        Flags,
        //install a subapp
        AppInstall,
        //update a subapp
        AppUpdate,
        //check for updates
        AppCheckUpdate,
        //start a subapp
        AppStart,
        //stop a subapp
        AppStop,
        //restart a subapp
        AppRestart,
        //uninstall a subapp
        AppUninstall,
        //configure a subapp
        AppConfig,
        //list all subapps
        AppList,
        //get overall status
        Status

        

    }
}
