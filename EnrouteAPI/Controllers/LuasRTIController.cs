using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using EnrouteAPI.LUAS;
using System.Xml.Serialization;

namespace EnrouteAPI.Controllers
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

                return JsonConvert.SerializeObject(luasStopData, Newtonsoft.Json.Formatting.Indented);
            }
        }
    }
}
