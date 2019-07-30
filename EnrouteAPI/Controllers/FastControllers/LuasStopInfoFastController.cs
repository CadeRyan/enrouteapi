using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace EnrouteAPI.Controllers.FastControllers
{
    [Route("api/[controller]")]
    public class LuasStopInfoFastController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Files\", "AllLuasStops.json"));
        }
    }
}
