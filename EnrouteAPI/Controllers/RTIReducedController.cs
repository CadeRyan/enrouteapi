using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class RTIReducedController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "server working for the RTI-reduced call, add a stop number" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            WebRequest request = WebRequest.Create($"https://data.smartdublin.ie/cgi-bin/rtpi/realtimebusinformation?stopid={id}&format=json");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                var rtq = RealTimeQuery.FromJson(sr.ReadToEnd());
                RealTimeQueryReduced rtqr = new RealTimeQueryReduced();
                rtqr.Numberofresults = rtq.Numberofresults;
                rtqr.Stopid = rtq.Stopid;
                rtqr.Results = new List<RealTimeQueryReduced.ResultReduced>();
                foreach (RealTimeQueryResult result in rtq.Results)
                {
                    rtqr.Results.Add(new RealTimeQueryReduced.ResultReduced(result.Duetime, result.Scheduledarrivaldatetime, result.Destination, result.Route));
                }
                return JsonConvert.SerializeObject(rtqr, Formatting.Indented);
            }
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
