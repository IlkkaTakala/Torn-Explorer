using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Converters;

namespace Project.Repository
{
    public class APIRepository : IRepository
    {
        private Dictionary<int, Item> _items = null;
        private List<RWar> _wars = null;
        private List<string> _itemtypes = null;
        private Profile _profile = null;

        private static readonly HttpClient _client = new HttpClient();

        private static readonly string API = "https://api.torn.com/";

        public async Task<Dictionary<int, Item>> GetItems()
        {
            if (_items == null)
            {
                await RefreshItems();
            }
            return _items;
        }

        public async Task<Dictionary<int, Item>> GetItems(FilterParams value)
        {
            return (await GetItems()).Where(
                i =>
                (value.Type == null || value.Type.Equals("All") || i.Value.Type.Equals(value.Type))
                && (value.Name == null || i.Value.Name.ToLower().Contains(value.Name.ToLower()))
            ).ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<List<string>> GetItemTypes()
        {
            return _itemtypes;
        }

        public async Task<Profile> GetProfile()
        {
            if (_profile == null)
            {
                await RefreshProfile();
            }
            return _profile;
        }

        public async Task<List<RWar>> GetWars()
        {
            if (_wars == null)
            {
                await RefreshWars();
            }
            return _wars;
        }

        public async Task RefreshItems()
        {
            try
            {
                var response = await _client.GetAsync($"{API}torn/?selections=items&key={RepositorySwitcher.GetAPI()}");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase);

                string json = await response.Content.ReadAsStringAsync();

                var token = JObject.Parse(json).SelectToken("items");
                if (token != null)
                    _items = token.ToObject<Dictionary<int, Item>>().Where((data) => !data.Value.Type.Equals("Unused")).ToDictionary(t => t.Key, t => t.Value);
                else throw new Exception("API down or invalid key");

                await RefreshItemTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Finding item list failed");
                _items = new Dictionary<int, Item>();
            }
        }

        public async Task RefreshItemTypes()
        {
            _itemtypes = (await GetItems()).Select(i => i.Value.Type).Distinct().ToList();
            _itemtypes.Insert(0, "All");
        }

        public async Task RefreshProfile()
        {
            try
            {
                var response = await _client.GetAsync($"{API}user/?selections=profile,networth&key={RepositorySwitcher.GetAPI()}");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase);

                string json = await response.Content.ReadAsStringAsync();

                _profile = JsonConvert.DeserializeObject<Profile>(json);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Finding profile failed");
            }
        }

        public async Task RefreshWars()
        {
            try
            {
                var response = await _client.GetAsync($"{API}torn/?selections=rankedwars&key={RepositorySwitcher.GetAPI()}");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException(response.ReasonPhrase);

                string json = await response.Content.ReadAsStringAsync();

                var token = JObject.Parse(json).SelectToken("rankedwars");
                if (token != null)
                _wars = token.ToObject<Dictionary<int, RWar>>().Values.Where(war => war.Data.End.CompareTo(MicrosecondEpochConverter._epoch) == 0).OrderBy(war => war.Starting).ToList();
                else throw new Exception("API down or invalid key");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Finding wars failed");
            }
        }
    }
}
