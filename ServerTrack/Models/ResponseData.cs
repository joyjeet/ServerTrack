using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    public class ResponseData
    {
        public double AvgCPULoad { get; set; }
        public double AvgRAMLoad { get; set; }
        public DateTime Time { get; set; }
    }
}