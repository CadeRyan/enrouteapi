using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers.FastControllers
{
    [Route("api/[controller]")]
    public class BusStopInfoFastController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Files\", "AllBusStops.json"));
        }
    }
}
