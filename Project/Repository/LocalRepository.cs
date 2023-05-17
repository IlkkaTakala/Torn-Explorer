using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class LocalRepository : IRepository
    {
        private Dictionary<int, Item> _items = null;
        private Dictionary<int, RWar> _wars = null;
        private List<string> _itemtypes = null;

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
            await Task.Delay(2000);
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("Project.Resources.sampleData.json")))
            {
                _items = JObject.Parse(await reader.ReadToEndAsync()).SelectToken("items").Children().OfType<JProperty>().ToDictionary(x => int.Parse(x.Name), x => x.Value.ToObject<Item>());
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

        public async Task<Dictionary<int, RWar>> GetWars()
        {
            if (_wars == null)
            {
                await RefreshWars();
            }
            return _wars;
        }
        public async Task RefreshWars()
        {
            await Task.Delay(2000);
            var assembly = Assembly.GetExecutingAssembly();
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("Project.Resources.sampleData.json")))
            {
                _wars = JObject.Parse(await reader.ReadToEndAsync()).SelectToken("rankedwars").Children().OfType<JProperty>().ToDictionary(x => int.Parse(x.Name), x => x.Value.ToObject<RWar>());
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
    }
}
