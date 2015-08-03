using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ServerTrack.Models;
using ServerTrack.Common;

namespace ServerTrack.Controllers
{
    public class DataController : ApiController
    {
        private readonly IDataRepository dataRepository;

        public DataController()
        {
            dataRepository = DataRepository.Instance;
        }

        public DataController(IDataRepository repository)
        {
            if(repository==null)
            {
                throw new ArgumentException("Server data is empty");
            }
            dataRepository = repository;
        }

        // GET: api/Data/ServerName
        private Dictionary<string, List<ResponseData>> Get(string serverName)
        {
            Dictionary<string, List<ResponseData>> data = new Dictionary<string, List<ResponseData>>();

            if (!string.IsNullOrEmpty(serverName))
            {
                // last 60 min, 1 min interval
                var lastHour = dataRepository.FilterData(serverName,60, 1); 
                data.Add("LastHour", lastHour);

                // last day, 1 hour interval
                var lastDay = dataRepository.FilterData(serverName,60 * 24, 60);
                data.Add("Lastday", lastDay);
            }
           
            return data;
        }

        public async Task<Dictionary<string, List<ResponseData>>> GetAcync(string serverName)
        {
            return await Task.FromResult(Get(serverName));
        }

        private HttpResponseMessage RecordLoad([FromBody]Data serverData)
        {
            IEnumerable<Data> data = null;

            if (serverData != null)
            {
                dataRepository.Add(serverData);    
            }

            return Request.CreateResponse(HttpStatusCode.Created, serverData);
        }

        // POST: api/Data/RecordLoad
        [HttpPost]
        public async Task<HttpResponseMessage> RecordLoadAsync([FromBody] Data serverData)
        {
            return await Task.FromResult(RecordLoad(serverData));
        }

    }
}
