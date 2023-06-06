using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace Project.ViewModel
{
    public class ItemsVM : ObservableObject
    {
		private Dictionary<int, Item> _items;
		private List<string> _itemtypes;
		
		public Dictionary<int, Item> Items
		{
			get { return _items; }
			set { _items = value; OnPropertyChanged("Items"); }
		}

        public List<string> ItemTypes
        {
            get { return _itemtypes; }
            set { _itemtypes = value; OnPropertyChanged("ItemTypes"); }
        }

		private string _selectedtype;

		public string SelectedType
		{
			get { return _selectedtype; }
			set {
				_selectedtype = value;
				FilterItems();
				OnPropertyChanged("SelectedType"); 
			}
		}

		private string _searchText;

		public string SearchText
		{
			get { return _searchText; }
			set { 
				_searchText = value;
				OnPropertyChanged("SelectedType");
				FilterItems();
            }
		}
		
		private string _warsearchText;

		public string WarSearchText
		{
			get { return _warsearchText; }
			set { 
				_warsearchText = value;
				OnPropertyChanged("SelectedType");
				FilterWars();
            }
		}

		private string _loadingString = "Loading...";

		public string LoadingString
		{
			get { return _loadingString; }
			set { _loadingString = value; OnPropertyChanged("LoadingString"); }
		}

		private List<RWar> _wars;

		public List<RWar> Wars
		{
			get { return _wars; }
			set { _wars = value; OnPropertyChanged("Wars"); }
		}

		private async Task GetItemsAsync()
		{
            Items = await RepositorySwitcher.GetRepository().GetItems();
            LoadingString = "";
        }
        private async Task GetWarsAsync()
		{
            Wars = await RepositorySwitcher.GetRepository().GetWars();
        }
        private async Task GetTypesAsync()
		{
			ItemTypes = await RepositorySwitcher.GetRepository().GetItemTypes();
		}

		private async void GetAllData()
		{
            List<Task> Tasks = new List<Task>();
            Tasks.Add(GetItemsAsync());
			Tasks.Add(GetWarsAsync());
			await Task.WhenAll(Tasks);
            GetTypesAsync();
        }

        private async void FilterItems()
		{ 
			LoadingString = "Loading...";
			Items = null;
            if (SearchText == null && SelectedType == null)
            {
				await GetItemsAsync();
			} else
			{
                Items = await RepositorySwitcher.GetRepository().GetItems(new FilterParams{Name=SearchText, Type=SelectedType});
				LoadingString = "";
            }
        }
        private async void FilterWars()
		{ 
			//Items = null;
            if (WarSearchText == null)
            {
				await GetWarsAsync();
			} else
			{
				var _war = await RepositorySwitcher.GetRepository().GetWars();
                Wars = _war.Where(w => w.First.Name.ToLower().Contains(WarSearchText.ToLower()) || w.Second.Name.ToLower().Contains(WarSearchText.ToLower())).ToList();
            }
        }

        public RelayCommand Filter { get; private set; }

		public ItemsVM() 
		{
			LoadingString = "Loading...";
			GetAllData();
			Filter = new RelayCommand(FilterItems);
			RepositorySwitcher.APIChangedDelegate += () => { 
				GetAllData();
			};
        }
    }
}
