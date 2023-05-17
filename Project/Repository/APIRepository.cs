using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class APIRepository : IRepository
    {
        private Dictionary<int, Item> _items = null;
        private Dictionary<int, RWar> _wars = null;
        private List<string> _itemtypes = null;
        public Task<Dictionary<int, Item>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, Item>> GetItems(FilterParams value)
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetItemTypes()
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<int, RWar>> GetWars()
        {
            throw new NotImplementedException();
        }

        public Task RefreshItems()
        {
            throw new NotImplementedException();
        }

        public Task RefreshItemTypes()
        {
            throw new NotImplementedException();
        }

        public Task RefreshWars()
        {
            throw new NotImplementedException();
        }
    }
}
