using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Registry.Models;
//
//    var serviceInfo = ServiceInfo.FromJson(jsonString);

namespace Registry.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ServiceInfo
    {
        public ServiceInfo(string name, string description, Uri apiEndpoint, int numberOfOperands, string operandType)
        {
            Name = name;
            Description = description;
            ApiEndpoint = apiEndpoint;
            NumberOfOperands = numberOfOperands;
            OperandType = operandType;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("API endpoint")]
        public Uri ApiEndpoint { get; set; }

        [JsonProperty("number of operands")]
        [JsonConverter(typeof(ParseStringConverter))]
        public int NumberOfOperands { get; set; }

        [JsonProperty("OperandType")]
        public string OperandType { get; set; }
    }
    public partial class ServiceInfo
    {
        public static List<ServiceInfo> FromJson(string json) => JsonConvert.DeserializeObject<List<ServiceInfo>>(json, Registry.Models.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<ServiceInfo> self) => JsonConvert.SerializeObject(self, Registry.Models.Converter.Settings);
    }

    internal static class Converter
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(int) || t == typeof(int);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            int l;
            if (Int32.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type int");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (int)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }
    }
}
