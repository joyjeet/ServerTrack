using ServerTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Common
{
    public class DataRepository:IDataRepository
    {
        private readonly List<Data>  Serverdata = new List<Data>();
        private static volatile DataRepository instance;
        private static object lockObj = new object();

        private DataRepository() { }

        public static DataRepository Instance
        {
            get
            {
                lock(lockObj)
                {
                    if(instance==null)
                    {
                        instance = new DataRepository();
                    }
                }

                return instance;
            }
        }

        public Data Add(Data serverData)
        {
            if(serverData==null)
            {
                throw new ArgumentNullException("ServerData");
            }

            Serverdata.Add(serverData);
            return serverData;
        }

        public List<ResponseData> FilterData(string serverName, int forTimeSpanInMins, int interval)
        {
            var data = Serverdata.Where(p => p.ServerName == serverName).Where(t1 => t1.Time > DateTime.Now.Subtract(TimeSpan.FromMinutes(forTimeSpanInMins))).GroupBy(t =>
            {
                var stamp = t.Time;
                stamp = stamp.AddMinutes(-(stamp.Minute % interval));
                stamp = stamp.AddMilliseconds(-stamp.Millisecond - 1000 * stamp.Second);
                return stamp;
            })
             .Select(g =>
             new ResponseData
             {
                 Time = g.Key,
                 AvgCPULoad = g.Average(p => p.CpuLoad),
                 AvgRAMLoad = g.Average(p => p.RamLoad),
             })
             .OrderBy(o => o.Time).ToList();
            return data;
        }
    }
}