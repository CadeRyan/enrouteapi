using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using EnrouteAPI.DublinBus;

namespace EnrouteAPI.Controllers.BusControllers
{
    [Route("api/[controller]")]
    public class BusRouteInfoController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
               (sender, cert, chain, sslPolicyErrors) => true;

            string busOperator = "";
            if (StaticInfo.bacRoutes.Contains(id)){
                busOperator = "bac";
            }
            else if (StaticInfo.gadRoutes.Contains(id))
            {
                busOperator = "gad";
            }
            else
            {
                //this shit will fail, handle appropriately
            }

            WebRequest request = WebRequest.Create($"https://data.smartdublin.ie/cgi-bin/rtpi/routeinformation?routeid={id}&operator={busOperator}&format=json");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                var brq = BusRouteQuery.FromJson(sr.ReadToEnd());
                BusRouteQueryReduced brqr = new BusRouteQueryReduced();
                brqr.Results = new List<BusRouteQueryReduced.ResultReduced>();

                int count1 = 0;
                int count2 = 0;
                string destination = "";
                string start = "";
                
                foreach (BusRouteQueryResult result in brq.Results)
                {
                    if (result.Lastupdated.Contains("/2019"))
                    {
                        if (result.Stops.Length > count1)
                        {
                            count2 = count1;
                            count1 = result.Stops.Length;
                            destination = result.Destination;
                            start = result.Origin;
                        }
                        else if(result.Stops.Length > count2)
                        {
                            count2 = result.Stops.Length;

                        }
                    }
                }

                brqr.Numberofresults = count1;
                brqr.Destination = destination;
                brqr.Origin = start;
                brqr.NotPlaces = new List<string>();
                foreach (BusRouteQueryResult result in brq.Results)
                {
                    var stops = new List<BusRouteQueryReduced.ResultReduced.StopReduced>();

                    if (result.Lastupdated.Contains("/2019"))
                    {
                        //if ((result.Destination.Equals(destination) && result.Origin.Equals(start))
                        //    || (result.Destination.Equals(start) && result.Origin.Equals(destination)))
                        if(result.Stops.Length == count1 || result.Stops.Length == count2)
                        {
                            foreach (Stop stop in result.Stops)
                            {
                                stops.Add(new BusRouteQueryReduced.ResultReduced.StopReduced(stop.Stopid, stop.Fullname, stop.Latitude, stop.Longitude));

                            }
                            brqr.Results.Add(new BusRouteQueryReduced.ResultReduced(result.Destination, stops));
                        }
                        else
                        {
                            if (result.Stops.Length >= 50)
                            {
                                brqr.NotCount = result.Stops.Length;
                                brqr.NotPlaces.Add(result.Destination);
                                //brf.NotPlaces.Add("test");
                            }
                        }
                    }
                }
                return JsonConvert.SerializeObject(brqr, Formatting.Indented);
            }
        }
    }
}
