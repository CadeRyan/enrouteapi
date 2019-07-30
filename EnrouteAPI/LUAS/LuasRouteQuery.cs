using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EnrouteAPI.LUAS
{
    [XmlRoot(ElementName = "stops")]
    public class LuasRouteQuery
    {
        [XmlElement(ElementName = "line")]
        public List<Line> Line { get; set; }
    }

    [XmlRoot(ElementName = "stop")]
    public class Stop
    {
        [XmlText]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "abrev")]
        public string Abrev { get; set; }
        [XmlAttribute(AttributeName = "isParkRide")]
        public string IsParkRide { get; set; }
        [XmlAttribute(AttributeName = "isCycleRide")]
        public string IsCycleRide { get; set; }
        [XmlAttribute(AttributeName = "lat")]
        public string Lat { get; set; }
        [XmlAttribute(AttributeName = "long")]
        public string Long { get; set; }
        [XmlAttribute(AttributeName = "pronunciation")]
        public string Pronunciation { get; set; }
    }

    [XmlRoot(ElementName = "line")]
    public class Line
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "stop")]
        public List<Stop> Stop { get; set; }
    }
}