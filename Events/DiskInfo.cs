using GalaSoft.MvvmLight;
using MrG.Daemon.Control.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrG.Daemon.Control.Events
{
    public class DiskInfo : ViewModelBase
    {
        [JsonProperty("name")]
        public string Path { get; set; } = "";

        private double _freeSpace;
        [JsonProperty("free")]
        public double FreeSpace
        {
            get { return _freeSpace; }
            set
            {
                Set(ref _freeSpace, value);
                RaisePropertyChanged(nameof(SpacePercentage));
                RaisePropertyChanged(nameof(FreeSpaceString));
            }
        }

        public string FreeSpaceString
        {
            get
            {
                return ConvertBytesToReadableSize(FreeSpace);
            }
        }

        private double _totalSpace;
        [JsonProperty("total")]
        public double TotalSpace
        {
            get { return _totalSpace; }
            set
            {
                Set(ref _totalSpace, value);
                RaisePropertyChanged(nameof(SpacePercentage));
                RaisePropertyChanged(nameof(TotalSpaceString));
            }
        }

        public string TotalSpaceString
        {
            get
            {
                return ConvertBytesToReadableSize(TotalSpace);
            }
        }

        [JsonIgnore]
        public int SpacePercentage
        {
            get
            {
                if (TotalSpace == 0)
                {
                    return 0;
                }
                return (int)(((TotalSpace-FreeSpace) / TotalSpace) * 100);

            }
        }
        public DiskEventEnum Type { get; set; }

        public DiskInfo Update(DiskInfo disk)
        {
            Path = disk.Path;
            FreeSpace = disk.FreeSpace;
            TotalSpace = disk.TotalSpace;
            Type = disk.Type;
            return this;
        }

        static string ConvertBytesToReadableSize(double byteCount)
        {
            string[] sizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
            {
                return "0 B";
            }

            int mag = (int)Math.Log(byteCount, 1024);
            double adjustedSize = byteCount / Math.Pow(1024, mag);

            return $"{adjustedSize:n2} {sizeSuffixes[mag]}";
        }
    }
}
