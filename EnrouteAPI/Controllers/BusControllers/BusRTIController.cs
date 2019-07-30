using System.Collections.Generic;
using System.IO;
using System.Net;
using EnrouteAPI.DublinBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers.BusControllers
{
    [Route("api/[controller]")]
    public class BusRTIController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int id)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
    
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
    }
}
