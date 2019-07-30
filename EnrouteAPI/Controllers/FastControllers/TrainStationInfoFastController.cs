using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace EnrouteAPI.Controllers.FastControllers
{
    [Route("api/[controller]")]
    public class TrainStationInfoFastController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Files\", "AllTrainStations.json"));
        }
    }
}
