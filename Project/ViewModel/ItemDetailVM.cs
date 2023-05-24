using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Model;

namespace Project.ViewModel
{
    public class ItemDetailVM : ObservableObject
    {
		private Item _item;

		public RelayCommand OpenMarket { get; set; }

		public Item Item
		{
			get { return _item; }
			set { _item = value; OnPropertyChanged("Item"); }
		}
		
		public ItemDetailVM() {
			OpenMarket = new RelayCommand(() => { Process.Start($"https://www.torn.com/imarket.php#/p=shop&step=shop&type=&searchname={_item.Name.Replace(" ", "_")}"); });
		}
	}
}
