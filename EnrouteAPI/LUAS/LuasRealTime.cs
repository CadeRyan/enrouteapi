
using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EnrouteAPI.LUAS
{
    [XmlRoot(ElementName = "tram")]
    public class Tram
    {
        [XmlAttribute(AttributeName = "destination")]
        public string Destination { get; set; }
        [XmlAttribute(AttributeName = "dueMins")]
        public string DueMins { get; set; }
    }

    [XmlRoot(ElementName = "direction")]
    public class Direction
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "tram")]
        public List<Tram> Tram { get; set; }
    }

    [XmlRoot(ElementName = "stopInfo")]
    public class LuasRealTime
    {
        [XmlAttribute(AttributeName = "created")]
        public string Created { get; set; }
        [XmlAttribute(AttributeName = "stop")]
        public string Stop { get; set; }
        [XmlAttribute(AttributeName = "stopAbv")]
        public string StopAbv { get; set; }
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }
        [XmlElement(ElementName = "direction")]
        public List<Direction> Direction { get; set; }
    }

}
