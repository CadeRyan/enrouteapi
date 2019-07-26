using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrouteAPI
{
    public class DartRTIOutput
    {
        public DartRTIOutput()
        {
            Results = new List<DartRTIResult>();
        }

        public List<DartRTIResult> Results { get; set; }

        public class DartRTIResult
        {
            public DartRTIResult(string traincode, string stationfullname, string stationcode,
            string origin, string destination, string origintime, string destinationtime,
            string status, string lastlocation, string duein, string late, string exparrival,
            string expdepart, string scharrival, string schdepart, string direction,
            string traintype, string locationtype)
            {
                Traincode = traincode;
                Stationfullname = stationfullname;
                Stationcode = stationcode;
                Origin = origin;
                Destination = destination;
                Origintime = origintime;
                Destinationtime = destinationtime;
                Status = status;
                Lastlocation = lastlocation;
                Duein = duein;
                Late = late;
                Exparrival = exparrival;
                Expdepart = expdepart;
                Scharrival = scharrival;
                Schdepart = schdepart;
                Direction = direction;
                Traintype = traintype;
                Locationtype = locationtype;
            }

            public string Traincode { get; set; }
            public string Stationfullname { get; set; }
            public string Stationcode { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public string Origintime { get; set; }
            public string Destinationtime { get; set; }
            public string Status { get; set; }
            public string Lastlocation { get; set; }
            public string Duein { get; set; }
            public string Late { get; set; }
            public string Exparrival { get; set; }
            public string Expdepart { get; set; }
            public string Scharrival { get; set; }
            public string Schdepart { get; set; }
            public string Direction { get; set; }
            public string Traintype { get; set; }
            public string Locationtype { get; set; }
        }
    }
}
