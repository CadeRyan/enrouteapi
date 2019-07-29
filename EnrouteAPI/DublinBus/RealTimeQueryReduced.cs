using System;
using System.Collections.Generic;

namespace EnrouteAPI.DublinBus
{
    public partial class RealTimeQueryReduced
    {
        public long Numberofresults { get; set; }
        public long Stopid { get; set; }
        public List<ResultReduced> Results { get; set; }

        public partial class ResultReduced
        {
            public ResultReduced(string duetime, string scheduledarrivaldatetime, string destination, string route)
            {
                Duetime = duetime;
                Scheduledarrivaldatetime = scheduledarrivaldatetime;
                Destination = destination;
                Route = route;
            }
            public string Duetime { get; set; }
            public string Scheduledarrivaldatetime { get; set; }
            public string Destination { get; set; }
            public string Route { get; set; }
        }
    }
}
