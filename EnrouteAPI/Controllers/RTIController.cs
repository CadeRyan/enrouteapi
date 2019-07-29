using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class RTIController : ControllerBase
    {
        [HttpGet("{id}")]
        public string Get(int id)
        {
            ServicePointManager
            .ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
    
            WebRequest request = WebRequest.Create($"https://data.smartdublin.ie/cgi-bin/rtpi/realtimebusinformation?stopid={id}&format=json");

            string text = "";
            using (var sr = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            return text;
        }
    }
}
