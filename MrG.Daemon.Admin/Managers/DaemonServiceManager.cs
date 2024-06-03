using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MrG.Daemon.Control.Managers
{
    public static class DaemonServiceManager
    {
        public static event EventHandler<ServiceControllerStatus?> ServiceStatusChanged;

        public static string ServiceName = "MrG.Daemon";

        public static bool AutoRestart { get; set; }

        private static int _intervalCheck = 5000;
        public static int IntervalCheck
        {
            get
            {
                return _intervalCheck;
            }
            set
            {
                _intervalCheck = value;
                CheckServiceStatusInit();
            }
        }

        static DaemonServiceManager() {
            CheckServiceStatusInit();


        }
        static System.Timers.Timer CheckServiceTimer = new System.Timers.Timer();
        private static void CheckServiceStatusInit()
        {
            if(CheckServiceTimer.Enabled)
            {
                CheckServiceTimer.Stop();
            }
            CheckServiceTimer.Interval = IntervalCheck;
            CheckServiceTimer.Elapsed += CheckServiceStatus;
            
            CheckServiceTimer.Start();

        }

        private static void CheckServiceStatus(object? sender, ElapsedEventArgs e)
        {
            var service = GetWindowsService();
            if(service != null)
            {
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    ServiceStatusChanged?.Invoke(null, ServiceControllerStatus.Stopped);
                    if (AutoRestart)
                    {
                        StartService();
                    }
                }
                else if (service.Status == ServiceControllerStatus.Running)
                {
                    ServiceStatusChanged?.Invoke(null, ServiceControllerStatus.Running);
                }
            }
            
        }

        public static void StopService()
        {
            var service = GetWindowsService();
            if (service != null && service.Status == ServiceControllerStatus.Running)
            {
                service.Stop();
                //trigger ServiceStatusChanged with stopping event
                ServiceStatusChanged?.Invoke(null, ServiceControllerStatus.StopPending);
                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                ServiceStatusChanged?.Invoke(null, ServiceControllerStatus.Stopped);
            }
        }

        public static void RestartService()
        {
            StopService();
            StartService();
        }


        public static void StartService()
        {
            var service = GetWindowsService();
            if (service!=null && service.Status == ServiceControllerStatus.Stopped)
            {
                service.Start();
                ServiceStatusChanged?.Invoke(null, ServiceControllerStatus.StartPending);
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                ServiceStatusChanged?.Invoke(null, ServiceControllerStatus.Running);
            }
        }

        internal static ServiceController? GetWindowsService()
        {
            //get windows service with name MrG.Daemon
            ServiceController service = new ServiceController(ServiceName);
            try
            {
                var status = service.Status;
            }
            catch
            {
                ServiceStatusChanged?.Invoke(null, null);
                return null;
            }
            return service;
        }

        internal static bool ServiceExists
        {
            get
            {
                return ServiceController.GetServices().Any(s => s.ServiceName == ServiceName);
            }
        }

        internal static bool ServiceRunning
        {
            get
            {
                var service = GetWindowsService();

                return service!=null && service.Status == ServiceControllerStatus.Running;
            }
        }

    }
}
