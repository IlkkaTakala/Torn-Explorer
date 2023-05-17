using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class FilterParams
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public interface IRepository
    {
        Task<Dictionary<int, Item>> GetItems();
        Task<Dictionary<int, Item>> GetItems(FilterParams value);
        Task<Dictionary<int, RWar>> GetWars();
        Task<List<string>> GetItemTypes();
        Task RefreshItems();
        Task RefreshWars();
        Task RefreshItemTypes();
    }
    public class RepositorySwitcher
    {

        private static string API_KEY = "";
        private static bool useLocal = true;

        private static LocalRepository local;
        private static APIRepository online;

        public static void SetAPI(string value)
        {
            API_KEY = value;
            useLocal = API_KEY == null;
        }

        public static IRepository GetRepository()
        {
            return useLocal ? local as IRepository : online as IRepository;
        }
        public static void SetupRepository()
        {
            local = new LocalRepository();
            online = new APIRepository();
        }

        public RepositorySwitcher() 
        { 
            
        }
    }
}
