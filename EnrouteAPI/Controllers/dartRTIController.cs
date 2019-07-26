using System.IO;
using System.Net;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class DartRTIController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(string id)
        {
            WebRequest request = WebRequest.Create($"http://api.irishrail.ie/realtime/realtime.asmx/getStationDataByNameXML?StationDesc={id}");

            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ArrayOfObjStationData));
                ArrayOfObjStationData arrayOfObjStationData = (ArrayOfObjStationData)serializer.Deserialize(sr);

                DartRTIOutput output = new DartRTIOutput();

                foreach(var result in arrayOfObjStationData.ObjStationData)
                {
                    output.Results.Add(new DartRTIOutput.DartRTIResult(result.Traincode, result.Stationfullname, result.Stationcode,
                        result.Origin, result.Destination, result.Origintime, result.Destinationtime, result.Status,
                        result.Lastlocation, result.Duein, result.Late, result.Exparrival, result.Expdepart, result.Scharrival,
                        result.Schdepart, result.Direction, result.Traintype, result.Locationtype));
                }

                return JsonConvert.SerializeObject(output, Formatting.Indented); ;
            }
        }
    }
}
