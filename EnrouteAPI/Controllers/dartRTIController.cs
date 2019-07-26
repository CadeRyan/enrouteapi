using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class dartRTIController : ControllerBase
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(String id)
        {
            //WebRequest request = WebRequest.Create($"http://api.irishrail.ie/realtime/realtime.asmx/getStationDataByNameXML?StationDesc={id}");

            //using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            //{
            //    var brf = BusRouteQuery.FromJson(sr.ReadToEnd());


            //    return JsonConvert.SerializeObject(brf, Formatting.Indented);

            //}

            var xmlDoc = new XmlDocument();
            xmlDoc.Load($"http://api.irishrail.ie/realtime/realtime.asmx/getStationDataByNameXML?StationDesc={id}");
            string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);

           
            return jsonText;

        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
