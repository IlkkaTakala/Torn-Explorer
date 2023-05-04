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
        public string Effect { get; set; }

        [JsonProperty("type ")]
        public string Type { get; set; }

        [JsonProperty("buy_price")]
        public int BuyPrice { get; set; }

        [JsonProperty("sell_price")]
        public int SellPrice { get; set; }

        [JsonProperty("market_value")]
        public int MarketValue { get; set; }

        [JsonProperty("circulation")]
        public int Circulation { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }
}
