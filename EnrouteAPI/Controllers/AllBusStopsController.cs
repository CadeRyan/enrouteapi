using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EnrouteAPI.Controllers
{
    [Route("api/[controller]")]
    public class AllBusStopsController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Files\", "AllBusStops.json"));
        }
    }
}
