using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTrack.Controllers;
using ServerTrack.Models;

namespace ServerTrack.Tests
{
    [TestClass]
    public class DataControllerTest
    {
        [TestMethod]
        public void GetWithNameTest()
        {
            RecordLoadMultiAsyncTest();

            DataController dc = new DataController();
            dc.Request = new HttpRequestMessage();
            dc.Configuration = new HttpConfiguration();

            DateTime currentTime = DateTime.Now;

            var defaultGet = dc.GetAcync("Server3");

            Assert.IsTrue(defaultGet != null);
        }

        private static string JsonHelper(IEnumerable<Data> expected, IEnumerable<ResponseData> defaultGet, out string actualJson)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var expectedJson = serializer.Serialize(expected);
            actualJson = serializer.Serialize(defaultGet);
            return expectedJson;
        }

        [TestMethod]
        public void RecordLoadTestAsync()
        {
            DataController dc = new DataController();
            dc.Request = new HttpRequestMessage();
            dc.Configuration = new HttpConfiguration();

            DateTime currenTime = DateTime.Now;
            Data data = new Data() { ServerName = "Server1", CpuLoad = 3, RamLoad = 4, Time = currenTime };

            var reponse = dc.RecordLoadAsync(data);

            var defaultGet = dc.GetAcync("Server1");

            Assert.Inconclusive();
        }

        [TestMethod]
        public void RecordLoadMultiAsyncTest()
        {
            DataController dc = new DataController();
            dc.Request = new HttpRequestMessage();
            dc.Configuration = new HttpConfiguration();

            Random rnd = new Random();
            for (int i = 0; i < 240; i++)
            {
                Data server = new Data();
                server.ServerName = "Server" + rnd.Next(1, 11);
                server.CpuLoad = rnd.Next(100);
                server.RamLoad = rnd.Next(65);
                server.Time = DateTime.Now.Subtract(TimeSpan.FromMinutes(i));

                dc.RecordLoadAsync(server);
            }

        }
    }
}
