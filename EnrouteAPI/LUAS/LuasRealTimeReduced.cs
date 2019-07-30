using System.Collections.Generic;

namespace EnrouteAPI.LUAS
{
    public class LuasRealTimeReduced
    {
        public LuasRealTimeReduced(string stop, string stopAbv, string message)
        {
            Stop = stop;
            StopAbv = stopAbv;
            Message = message;
            Trams = new List<TramReduced>();
        }
        public string Stop { get; set; }
        public string StopAbv { get; set; }
        public string Message { get; set; }
        public List<TramReduced> Trams { get; set; }

        public class TramReduced
        {
            public TramReduced(string destination, string dueMins, string direction)
            {
                Destination = destination;
                DueMins = dueMins;
                Direction = direction;
            }
            public string Destination { get; set; }
            public string DueMins { get; set; }
            public string Direction { get; set; }
        }
    }
}
