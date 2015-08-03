using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    public class Data
    {
        public string ServerName { get; set; }
        public double CpuLoad { get; set; }
        public double RamLoad { get; set; }
        public DateTime Time { get; set; }
    }
}