using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class RouteInfoController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Server working, routeinfo" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {

            WebRequest request = WebRequest.Create($"https://data.smartdublin.ie/cgi-bin/rtpi/routeinformation?routeid={id}&operator=bac&format=json");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                var brq = BusRouteQuery.FromJson(sr.ReadToEnd());
                BasicRouteFiltered brf = new BasicRouteFiltered();
                //brf.Numberofresults = brq.Numberofresults;
                //brf.Route = brq.Route;
                brf.Results = new List<BasicRouteFiltered.ResultReduced>();

                int count1 = 0;
                string destination = "";
                string start = "";
                
                foreach (BusRouteQueryResult result in brq.Results)
                {
                    if (result.Lastupdated.Contains("/2019"))
                    {
                        if (result.Stops.Length > count1)
                        {
                            //count2 = count1;
                            count1 = result.Stops.Length;
                            destination = result.Destination;
                            start = result.Origin;
                        }
                    }
                   
                }

                brf.Numberofresults = count1;
                brf.Destination = destination;
                brf.Origin = start;
                brf.NotPlaces = new List<string>();
                foreach (BusRouteQueryResult result in brq.Results)
                {
                    var stops = new List<BasicRouteFiltered.ResultReduced.StopReduced>();

                    if (result.Lastupdated.Contains("/2019"))
                    {
                        if ((result.Destination.Equals(destination) && result.Origin.Equals(start))
                            || (result.Destination.Equals(start) && result.Origin.Equals(destination)))
                        {
                            foreach (Stop stop in result.Stops)
                            {
                                stops.Add(new BasicRouteFiltered.ResultReduced.StopReduced(stop.Stopid, stop.Fullname, stop.Latitude, stop.Longitude));

                            }
                            brf.Results.Add(new BasicRouteFiltered.ResultReduced(result.Destination, stops));
                        }
                        else
                        {
                            if (result.Stops.Length >= 50)
                            {
                                brf.NotCount = result.Stops.Length;
                                brf.NotPlaces.Add(result.Destination);
                                //brf.NotPlaces.Add("test");
                            }
                        }
                    }
                  
                    
                }
                return JsonConvert.SerializeObject(brf, Formatting.Indented);
            }
            return ("DWD");
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
