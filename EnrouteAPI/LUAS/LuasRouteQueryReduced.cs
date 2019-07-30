using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EnrouteAPI.LUAS
{
    public class LuasRouteQueryReduced
    {
        public LuasRouteQueryReduced()
        {
            Stops = new List<StopReduced>();
        }
        public List<StopReduced> Stops { get; set; }
    }

    public class StopReduced
    {
        public StopReduced(string text, string abrev, string line, string lat, string _long)
        {
            Text = text;
            Abrev = abrev;
            Line = line;
            Lat = lat;
            Long = _long;
        }
        public string Text { get; set; }
        public string Abrev { get; set; }
        public string Line { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
    }
}