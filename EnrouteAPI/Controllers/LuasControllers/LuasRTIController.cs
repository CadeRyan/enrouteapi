using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EnrouteAPI.LUAS;
using System.Xml.Serialization;

namespace EnrouteAPI.Controllers.LuasControllers
{
    [Route("api/[controller]")]
    public class LuasRTIController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;

            WebRequest request = WebRequest.Create($"http://luasforecasts.rpa.ie/xml/get.ashx?action=forecast&stop={id}&encrypt=false");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LuasRealTime));
                LuasRealTime luasStopData = (LuasRealTime)serializer.Deserialize(sr);
                LuasRealTimeReduced output = new LuasRealTimeReduced(luasStopData.Stop, luasStopData.StopAbv, luasStopData.Message);

                foreach (Direction direction in luasStopData.Direction)
                {
                    foreach(Tram tram in direction.Tram)
                    {
                        output.Trams.Add(new LuasRealTimeReduced.TramReduced(tram.Destination, tram.DueMins, direction.Name));
                    }
                }

                return JsonConvert.SerializeObject(output, Formatting.Indented);
            }
        }
    }
}
