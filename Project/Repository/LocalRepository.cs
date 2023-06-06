using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class LocalRepository : IRepository
    {
        private Dictionary<int, Item> _items = null;
        private List<RWar> _wars = null;
        private List<string> _itemtypes = null;
        private Profile _profile = null;

        async public Task<Dictionary<int, Item>> GetItems()
        {
            if (_items == null)
            {
                await RefreshItems();
            }
            return _items;
        }

        async public Task RefreshItems()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("Project.Resources.sampleData.json")))
            {
                _items = JObject.Parse(await reader.ReadToEndAsync()).SelectToken("items").ToObject<Dictionary<int, Item>>();
            }
        }

        public async Task<Dictionary<int, Item>> GetItems(FilterParams value)
        {
            return (await GetItems()).Where(
                i => 
                (value.Type == null || value.Type.Equals("All") || i.Value.Type.Equals(value.Type)) 
                && (value.Name == null || i.Value.Name.ToLower().Contains(value.Name.ToLower()))
            ).ToDictionary(x => x.Key, x => x.Value);
        }

        public async Task<List<RWar>> GetWars()
        {
            if (_wars == null)
            {
                await RefreshWars();
            }
            return _wars;
        }
        public async Task RefreshWars()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("Project.Resources.sampleData.json")))
            {
                _wars = JObject.Parse(await reader.ReadToEndAsync()).SelectToken("rankedwars").ToObject<Dictionary<int, RWar>>().Values.Where(war => war.Data.End.CompareTo(MicrosecondEpochConverter._epoch) == 0).OrderBy(war => war.Starting).ToList();
            }
        }

        public async Task<List<string>> GetItemTypes()
        {
            if (_itemtypes == null)
            {
                await RefreshItemTypes();
            }
            return _itemtypes;
        }
        public async Task RefreshItemTypes()
        {
            _itemtypes = (await GetItems()).Select(i => i.Value.Type).Distinct().ToList();
            _itemtypes.Insert(0, "All");
        }

        public async Task<Profile> GetProfile()
        {
            if (_profile == null)
            {
                await RefreshProfile();
            }
            return _profile;
        }

        public async Task RefreshProfile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("Project.Resources.profileData.json")))
            {
                _profile = JsonConvert.DeserializeObject<Profile>(await reader.ReadToEndAsync());
            }
        }
    }
}
