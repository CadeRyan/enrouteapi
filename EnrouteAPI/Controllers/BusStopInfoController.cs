using System;
using System.IO;
using System.Linq;
using System.Net;
using EnrouteAPI.DublinBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class BusStopInfoController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            AllStopsOutput output = new AllStopsOutput();

            foreach (string stop in StaticInfo.allRoutes)
            {
                string op = GetOperator(stop);

                WebRequest request = WebRequest.Create($"https://data.smartdublin.ie/cgi-bin/rtpi/routeinformation?routeid={stop}&operator={op}&format=json");
                using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    BusRouteQuery data = BusRouteQuery.FromJson(sr.ReadToEnd());

                    foreach (BusRouteQueryResult routeData in data.Results)
                    {
                        foreach (Stop busStopData in routeData.Stops)
                        {
                            if (!AlreadyContainsID(output, busStopData))
                            {
                                output.BusStopDataResults.Add(new AllStopsOutput.StopReduced(busStopData.Stopid, busStopData.Shortname,
                                    busStopData.Fullname, busStopData.Latitude, busStopData.Longitude));
                            }
                        }
                    }
                }
            }
            output.StopCount = output.BusStopDataResults.Count;
            return JsonConvert.SerializeObject(output, Formatting.Indented);
        }

        private static string GetOperator(string stop)
        {
            if (StaticInfo.bacRoutes.Contains(stop))
            {
                return "bac";
            }
            else
            {
                return "gad";
            }
        }

        private static bool AlreadyContainsID(AllStopsOutput output, Stop stop)
        {
            foreach (AllStopsOutput.StopReduced alreadyStored in output.BusStopDataResults)
            {
                if (alreadyStored.Stopid == stop.Stopid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
