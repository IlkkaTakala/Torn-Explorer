using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Model
{
    public class Item
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("effect")]
        private string effect { get; set; }

        public string Effect { get { return $"Effect: {effect}"; } }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("buy_price")]
        public Int64 BuyPrice { get; set; }

        [JsonProperty("sell_price")]
        public Int64 SellPrice { get; set; }

        [JsonProperty("market_value")]
        public Int64 MarketValue { get; set; }

        [JsonProperty("circulation")]
        public Int64 Circulation { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
