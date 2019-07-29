using System;
using System.Collections.Generic;

namespace EnrouteAPI.DublinBus
{
    public partial class BasicRouteFiltered
    {
        public int Numberofresults { get; set; }
        public int Route { get; set; }
        public int Count2 { get; set; }
        public int NotCount { get; set; }

        public string Destination { get; set; }
        public string Origin { get; set; }
        public List<string> NotPlaces { get; set; }


        public List<ResultReduced> Results { get; set; }

        public partial class ResultReduced
        {

            public ResultReduced(string destination, List<StopReduced> stops)
            {
                RouteDestination = destination;
                Stops = stops;
            }

            public string RouteDestination { get; set; }
            public List<StopReduced> Stops { get; set; }

            public partial class StopReduced
            {
                public StopReduced(long stopid, string address, string lat, string lon)
                {
                    StopID = stopid;
                    Address = address;
                    Lat = lat;
                    Lon = lon;
                }
                public long StopID { get; set; }
                public string Address { get; set; }
                public string Lat { get; set; }
                public string Lon { get; set; }
            }
        }
    }
}
