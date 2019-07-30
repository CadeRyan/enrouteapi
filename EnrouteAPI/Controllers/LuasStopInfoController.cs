using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using EnrouteAPI.LUAS;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class LuasStopInfoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;

            WebRequest request = WebRequest.Create($"http://luasforecasts.rpa.ie/xml/get.ashx?action=stops&encrypt=false");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LuasRouteQuery));
                LuasRouteQuery luasStopsData = (LuasRouteQuery)serializer.Deserialize(sr);

                LuasRouteQueryReduced output = new LuasRouteQueryReduced();

                foreach (Line line in luasStopsData.Line)
                {
                    foreach (Stop stop in line.Stop)
                    {
                        output.Stops.Add(new StopReduced(stop.Text, stop.Abrev, line.Name, stop.Lat, stop.Long));
                    }
                }
                return JsonConvert.SerializeObject(output, Formatting.Indented);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id}";
        }
    }
}
