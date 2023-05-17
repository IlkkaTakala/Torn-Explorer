using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Project.Model;

namespace Project.ViewModel
{
    public class ItemDetailVM : ObservableObject
    {
		private Item _item;

		public Item Item
		{
			get { return _item; }
			set { _item = value; OnPropertyChanged("Item"); }
		}

	}
}
