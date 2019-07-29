namespace EnrouteAPI.DublinBus
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class RealTimeQuery
    {
        [JsonProperty("errorcode")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Errorcode { get; set; }

        [JsonProperty("errormessage")]
        public string Errormessage { get; set; }

        [JsonProperty("numberofresults")]
        public long Numberofresults { get; set; }

        [JsonProperty("stopid")]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Stopid { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("results")]
        public List<RealTimeQueryResult> Results { get; set; }
    }

    public partial class RealTimeQueryResult
    {
        [JsonProperty("arrivaldatetime")]
        public string Arrivaldatetime { get; set; }

        [JsonProperty("duetime")]
        public string Duetime { get; set; }

        [JsonProperty("departuredatetime")]
        public string Departuredatetime { get; set; }

        [JsonProperty("departureduetime")]
        public string Departureduetime { get; set; }

        [JsonProperty("scheduledarrivaldatetime")]
        public string Scheduledarrivaldatetime { get; set; }

        [JsonProperty("scheduleddeparturedatetime")]
        public string Scheduleddeparturedatetime { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("destinationlocalized")]
        public string Destinationlocalized { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("originlocalized")]
        public string Originlocalized { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("additionalinformation")]
        public string Additionalinformation { get; set; }

        [JsonProperty("lowfloorstatus")]
        public string Lowfloorstatus { get; set; }

        [JsonProperty("route")]
        public string Route { get; set; }

        [JsonProperty("sourcetimestamp")]
        public string Sourcetimestamp { get; set; }

        [JsonProperty("monitored")]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public bool Monitored { get; set; }
    }

    public partial class RealTimeQuery
    {
        public static RealTimeQuery FromJson(string json) => JsonConvert.DeserializeObject<RealTimeQuery>(json, EnrouteAPI.DublinBus.ConverterRTI.Settings);
    }

    public static class SerializeRTI
    {
        public static string ToJson(this RealTimeQuery self) => JsonConvert.SerializeObject(self, EnrouteAPI.DublinBus.ConverterRTI.Settings);
    }

    internal static class ConverterRTI
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class PurpleParseStringConverter : JsonConverter
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

        public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
    }

    internal class FluffyParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(bool) || t == typeof(bool?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
    }
}
