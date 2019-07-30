using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace EnrouteAPI.Train
{
    [XmlRoot(ElementName = "objStation", Namespace = "http://api.irishrail.ie/realtime/")]
    public class Station
    {
        [XmlElement(ElementName = "StationDesc", Namespace = "http://api.irishrail.ie/realtime/")]
        public string StationDesc { get; set; }
        [XmlElement(ElementName = "StationAlias", Namespace = "http://api.irishrail.ie/realtime/")]
        public string StationAlias { get; set; }
        [XmlElement(ElementName = "StationLatitude", Namespace = "http://api.irishrail.ie/realtime/")]
        public string StationLatitude { get; set; }
        [XmlElement(ElementName = "StationLongitude", Namespace = "http://api.irishrail.ie/realtime/")]
        public string StationLongitude { get; set; }
        [XmlElement(ElementName = "StationCode", Namespace = "http://api.irishrail.ie/realtime/")]
        public string StationCode { get; set; }
        [XmlElement(ElementName = "StationId", Namespace = "http://api.irishrail.ie/realtime/")]
        public string StationId { get; set; }
    }

    [XmlRoot(ElementName = "ArrayOfObjStation", Namespace = "http://api.irishrail.ie/realtime/")]
    public class TrainStationInfo
    {
        [XmlElement(ElementName = "objStation", Namespace = "http://api.irishrail.ie/realtime/")]
        public List<Station> Stations { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}