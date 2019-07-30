using System.IO;
using System.Net;
using System.Xml.Serialization;
using EnrouteAPI.Train;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers.TrainControllers
{
    [Route("api/[controller]")]
    public class TrainRTIController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            WebRequest request = WebRequest.Create($"http://api.irishrail.ie/realtime/realtime.asmx/getStationDataByNameXML?StationDesc={id}");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TrainRTI));
                TrainRTI trainRTI = (TrainRTI)serializer.Deserialize(sr);

                TrainRTIReduced output = new TrainRTIReduced();

                foreach(var result in trainRTI.RTIData)
                {
                    output.Results.Add(new TrainRTIReduced.TrainRTIResult(result.Traincode, result.Stationfullname, result.Stationcode,
                        result.Origin, result.Destination, result.Origintime, result.Destinationtime, result.Status,
                        result.Lastlocation, result.Duein, result.Late, result.Exparrival, result.Expdepart, result.Scharrival,
                        result.Schdepart, result.Direction, result.Traintype, result.Locationtype));
                }

                return JsonConvert.SerializeObject(output, Formatting.Indented); ;
            }
        }
    }
}
