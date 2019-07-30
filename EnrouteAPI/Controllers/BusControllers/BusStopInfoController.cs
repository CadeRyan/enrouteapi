using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EnrouteAPI.DublinBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers.BusControllers
{
    [Route("api/[controller]")]
    public class BusStopInfoController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            List<BusRouteQuery> allRoutes = new List<BusRouteQuery>();

            Parallel.ForEach(StaticInfo.allRoutes, (stop) =>
            {
                string op = GetOperator(stop);

                WebRequest request = WebRequest.Create($"https://data.smartdublin.ie/cgi-bin/rtpi/routeinformation?routeid={stop}&operator={op}&format=json");
                using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    allRoutes.Add(BusRouteQuery.FromJson(sr.ReadToEnd()));
                }
            });

            AllStopsOutput output = new AllStopsOutput();

            foreach (BusRouteQuery data in allRoutes)
            {
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

            output.StopCount = output.BusStopDataResults.Count;
            return JsonConvert.SerializeObject(output, Formatting.Indented);
        }

        private static string GetOperator(string stop)
        {
            switch (StaticInfo.gadRoutes.Contains(stop))
            {
                case true: return "gad";
                default: return "bac";
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
