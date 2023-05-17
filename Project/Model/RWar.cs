using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project.ViewModel.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Project.Model
{
    public class MicrosecondEpochConverter : DateTimeConverterBase
    {
        public static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
        public string Name { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("chain")]
        public int Chain { get; set; }

        public string Data
        {
            get
            {
                return $"Chain: {Chain}, Score: {Score}";
            }
        }
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

        public RFaction First { 
            get 
            {
                return Factions.First().Value;
            } 
        }
        public RFaction Second
        {
            get
            {
                return Factions.Last().Value;
            }
        }
        public Brush FirstColor
        {
            get
            {
                if (First.Score > Second.Score)
                {
                    return Brushes.Green;
                }
                if (First.Score < Second.Score)
                {
                    return Brushes.Red;
                }
                return Brushes.Gray;
            }
        }
        public Brush SecondColor
        {
            get
            {
                if (Second.Score > First.Score)
                {
                    return Brushes.Green;
                }
                if (Second.Score < First.Score)
                {
                    return Brushes.Red;
                }
                return Brushes.Gray;
            }
        }
        public string FirstUrl
        {
            get
            {
                return $"https://www.torn.com/factions.php?step=profile&ID={Factions.First().Key}";
            }
        }
        public string SecondUrl
        {
            get
            {
                return $"https://www.torn.com/factions.php?step=profile&ID={Factions.Last().Key}";
            }
        }

        public string Goal
        {
            get
            {
                return $"{Math.Abs(First.Score - Second.Score)}/{Data.Target}";
            }
        }
        public string Starting
        {
            get
            {
                if (Data.Start > DateTime.Now.ToUniversalTime())
                {
                    return "Starting in: " + (DateTime.Now.ToUniversalTime() - Data.Start).ToString("d'd 'h'h 'm'm 's's'");
                }
                return "Active for: " + (Data.Start - DateTime.Now.ToUniversalTime()).ToString("d'd 'h'h 'm'm 's's'");
            }
        }

        public RFaction Winner { get
            {
                if (Data.Winner == 0) return null;
                return Factions[Data.Winner];
            }
        }
    }
}
