using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EnrouteAPI.Train;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class TrainStationInfoController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get()
        {
            WebRequest request = WebRequest.Create($"http://api.irishrail.ie/realtime/realtime.asmx/getAllStationsXML");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TrainStationInfo));
                TrainStationInfo stationData = (TrainStationInfo)serializer.Deserialize(sr);

                TrainStationInfoReduced output = new TrainStationInfoReduced();

                foreach (var station in stationData.Stations)
                {
                    output.Stations.Add(new TrainStationInfoReduced.StationReduced(station.StationDesc, station.StationCode, station.StationLatitude, station.StationLongitude, station.StationId));
                }

                return JsonConvert.SerializeObject(output, Formatting.Indented); ;
            }
        }

        // GET api/<controller>/<routetype>
        [HttpGet("{id}")]
        public string Get(string id)
        {
            string type = GetType(id);
            WebRequest request = WebRequest.Create($"http://api.irishrail.ie/realtime/realtime.asmx/getAllStationsXML_WithStationType?StationType={type}");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TrainStationInfo));
                TrainStationInfo stationData = (TrainStationInfo)serializer.Deserialize(sr);

                TrainStationInfoReduced output = new TrainStationInfoReduced();

                foreach (var station in stationData.Stations)
                {
                    output.Stations.Add(new TrainStationInfoReduced.StationReduced(station.StationDesc, station.StationCode, station.StationLatitude, station.StationLongitude, station.StationId));
                }

                return JsonConvert.SerializeObject(output, Formatting.Indented); ;
            }
        }

        private string GetType(string id)
        {
            switch (id.ToLower())
            {
                case "dart": return "d";
                case "mainline": return "m";
                case "suburban": return "s";
                default: return "a";
            }
        }
    }
}
