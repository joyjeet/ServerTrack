using ServerTrack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Common
{
    public interface IDataRepository
    {
        Data Add(Data serverData);
        List<ResponseData> FilterData(string serverName, int forTimeSpanInMins, int interval);
    }
}