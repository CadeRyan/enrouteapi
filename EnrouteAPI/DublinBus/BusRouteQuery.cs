﻿namespace EnrouteAPI.DublinBus
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BusRouteQuery
    {
        [JsonProperty("errorcode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Errorcode { get; set; }

        [JsonProperty("errormessage")]
        public string Errormessage { get; set; }

        [JsonProperty("numberofresults")]
        public long Numberofresults { get; set; }

        [JsonProperty("route")]
        public string Route { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("results")]
        public BusRouteQueryResult[] Results { get; set; }
    }

    public partial class BusRouteQueryResult
    {
        [JsonProperty("operator")]
        public OperatorEnum Operator { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("originlocalized")]
        public string Originlocalized { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("destinationlocalized")]
        public string Destinationlocalized { get; set; }

        [JsonProperty("lastupdated")]
        public string Lastupdated { get; set; }

        [JsonProperty("stops")]
        public Stop[] Stops { get; set; }
    }

    public partial class Stop
    {
        [JsonProperty("stopid")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Stopid { get; set; }

        [JsonProperty("displaystopid")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Displaystopid { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("shortnamelocalized")]
        public string Shortnamelocalized { get; set; }

        [JsonProperty("fullname")]
        public string Fullname { get; set; }

        [JsonProperty("fullnamelocalized")]
        public string Fullnamelocalized { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("operators")]
        public OperatorElement[] Operators { get; set; }
    }

    public partial class OperatorElement
    {
        [JsonProperty("name")]
        public OperatorEnum Name { get; set; }

        [JsonProperty("routes")]
        public string[] Routes { get; set; }
    }

    public enum OperatorEnum { Bac, Gad };

    public partial class BusRouteQuery
    {
        public static BusRouteQuery FromJson(string json) => JsonConvert.DeserializeObject<BusRouteQuery>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BusRouteQuery self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                OperatorEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class OperatorEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OperatorEnum) || t == typeof(OperatorEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "GAD":
                    return OperatorEnum.Gad;
                case "bac":
                    return OperatorEnum.Bac;
            }
            throw new Exception("Cannot unmarshal type OperatorEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OperatorEnum)untypedValue;
            switch (value)
            {
                case OperatorEnum.Gad:
                    serializer.Serialize(writer, "GAD");
                    return;
                case OperatorEnum.Bac:
                    serializer.Serialize(writer, "bac");
                    return;
            }
            throw new Exception("Cannot marshal type OperatorEnum");
        }

        public static readonly OperatorEnumConverter Singleton = new OperatorEnumConverter();
    }
}
