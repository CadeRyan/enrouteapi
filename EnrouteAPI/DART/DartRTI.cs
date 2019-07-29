using System.Collections.Generic;
using System.Xml.Serialization;

namespace EnrouteAPI.DART
{
    [XmlRoot(ElementName = "objStationData", Namespace = "http://api.irishrail.ie/realtime/")]
    public class ObjStationData
    {
        [XmlElement(ElementName = "Servertime", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Servertime { get; set; }
        [XmlElement(ElementName = "Traincode", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Traincode { get; set; }
        [XmlElement(ElementName = "Stationfullname", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Stationfullname { get; set; }
        [XmlElement(ElementName = "Stationcode", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Stationcode { get; set; }
        [XmlElement(ElementName = "Querytime", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Querytime { get; set; }
        [XmlElement(ElementName = "Traindate", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Traindate { get; set; }
        [XmlElement(ElementName = "Origin", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Origin { get; set; }
        [XmlElement(ElementName = "Destination", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Destination { get; set; }
        [XmlElement(ElementName = "Origintime", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Origintime { get; set; }
        [XmlElement(ElementName = "Destinationtime", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Destinationtime { get; set; }
        [XmlElement(ElementName = "Status", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Status { get; set; }
        [XmlElement(ElementName = "Lastlocation", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Lastlocation { get; set; }
        [XmlElement(ElementName = "Duein", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Duein { get; set; }
        [XmlElement(ElementName = "Late", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Late { get; set; }
        [XmlElement(ElementName = "Exparrival", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Exparrival { get; set; }
        [XmlElement(ElementName = "Expdepart", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Expdepart { get; set; }
        [XmlElement(ElementName = "Scharrival", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Scharrival { get; set; }
        [XmlElement(ElementName = "Schdepart", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Schdepart { get; set; }
        [XmlElement(ElementName = "Direction", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Direction { get; set; }
        [XmlElement(ElementName = "Traintype", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Traintype { get; set; }
        [XmlElement(ElementName = "Locationtype", Namespace = "http://api.irishrail.ie/realtime/")]
        public string Locationtype { get; set; }
    }

    [XmlRoot(ElementName = "ArrayOfObjStationData", Namespace = "http://api.irishrail.ie/realtime/")]
    public class ArrayOfObjStationData
    {
        [XmlElement(ElementName = "objStationData", Namespace = "http://api.irishrail.ie/realtime/")]
        public List<ObjStationData> ObjStationData { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }

}
