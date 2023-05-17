using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class MicrosecondEpochConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds + "000");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            return _epoch.AddSeconds((long)reader.Value);
        }
    }
    public class RFaction
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("score")]
        public int Score;

        [JsonProperty("chain")]
        public int Chain;
    }

    public class WarData
    {
        [JsonProperty("start"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Start { get; set; }

        [JsonProperty("end"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime End { get; set; }

        [JsonProperty("target")]
        public int Target { get; set; }

        [JsonProperty("winner")]
        public int Winner { get; set; }
    }
    public class RWar
    {
        [JsonProperty("factions")]
        public Dictionary<int, RFaction> Factions { get; set; }

        [JsonProperty("war")]
        public WarData Data { get; set; }
       
    }
}
