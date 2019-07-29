using System;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class AllBusStopsController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            string[] dbStops = { "1", "4", "7", "7a", "7b", "7d", "7n", "9", "11", "13", "14", "15", "15a", "15b", "15d", "15n", "16", "25", "25a", "25d", "25b", "25n", "25x", "26", "27", "27b", "27a", "27x", "29a", "29n", "31a", "31b", "31d", "31n", "32", "32x", "33", "33d", "33n", "33x", "37", "38", "38a", "38b", "39", "39a", "39x", "39n", "40", "40b", "40d", "40e", "41", "41b", "41c", "41n", "41x", "42", "42d", "42n", "43", "44", "44b", "46a", "46e", "46n", "47", "49", "49n", "51d", "51x", "53", "53a", "54a", "56a", "61", "65", "65b", "66", "66e", "66a", "66b", "66n", "66x", "67", "67n", "67x", "68a", "68x", "69", "69n", "69x", "70", "70d", "70n", "77a", "77n", "77x", "79a", "83", "84a", "84n", "84x", "88n", "90", "116", "118", "120", "122", "123", "130", "140", "142", "145", "151", "155", "747", "757" };
            string[] gadStops = { "17", "17a", "18", "33a", "33b", "45a", "45b", "59", "63", "63a", "75", "75a", "76", "76a", "102", "104", "111", "114", "161", "175", "184", "185", "220", "236", "238", "239", "270" };
            string[] allStops = { "1", "4", "7", "7a", "7b", "7d", "7n", "9", "11", "13", "14", "15", "15a", "15b", "15d", "15n", "16", "25", "25a", "25d", "25b", "25n", "25x", "26", "27", "27b", "27a", "27x", "29a", "29n", "31a", "31b", "31d", "31n", "32", "32x", "33", "33d", "33n", "33x", "37", "38", "38a", "38b", "39", "39a", "39x", "39n", "40", "40b", "40d", "40e", "41", "41b", "41c", "41n", "41x", "42", "42d", "42n", "43", "44", "44b", "46a", "46e", "46n", "47", "49", "49n", "51d", "51x", "53", "53a", "54a", "56a", "61", "65", "65b", "66", "66e", "66a", "66b", "66n", "66x", "67", "67n", "67x", "68a", "68x", "69", "69n", "69x", "70", "70d", "70n", "77a", "77n", "77x", "79a", "83", "84a", "84n", "84x", "88n", "90", "116", "118", "120", "122", "123", "130", "140", "142", "145", "151", "155", "747", "757", "17", "17a", "18", "33a", "33b", "45a", "45b", "59", "63", "63a", "75", "75a", "76", "76a", "102", "104", "111", "114", "161", "175", "184", "185", "220", "236", "238", "239", "270" };

            AllStopsOutput output = new AllStopsOutput();

            foreach (string stop in allStops)
            {
                string op = "";

                if (dbStops.Contains(stop)) op = "bac";
                else op = "gad";

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
