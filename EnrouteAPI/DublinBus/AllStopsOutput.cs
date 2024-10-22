﻿using System.Collections.Generic;

namespace EnrouteAPI.DublinBus
{
    class AllStopsOutput
    {
        public AllStopsOutput()
        {
            BusStopDataResults = new List<StopReduced>();
        }
        public int StopCount { get; set; }
        public List<StopReduced> BusStopDataResults { get; set; }

        public class StopReduced
        {
            public StopReduced(long stopid, string shortname, string fullname, string latitude, string longitude)
            {
                Stopid = stopid;
                Shortname = shortname;
                Fullname = fullname;
                Latitude = latitude;
                Longitude = longitude;
            }
            public long Stopid { get; set; }
            public string Shortname { get; set; }
            public string Fullname { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
    }
}
