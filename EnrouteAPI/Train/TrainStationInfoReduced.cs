using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnrouteAPI.Train
{
    public class TrainStationInfoReduced
    {
        public TrainStationInfoReduced()
        {
            Stations = new List<StationReduced>();
        }
        public List<StationReduced> Stations { get; set; }

        public class StationReduced
        {
            public StationReduced(string stationDesc, string stationCode, string stationLatitude, string stationLongitude, string stationId)
            {
                Name = stationDesc;
                Code = stationCode;
                Latitude = stationLatitude;
                Longitude = stationLongitude;
                Id = stationId;
            }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Id { get; set; }
        }
    }
}
