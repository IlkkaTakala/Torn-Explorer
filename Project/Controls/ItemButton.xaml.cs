using Project.Repository;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.Controls
{
    /// <summary>
    /// Interaction logic for ItemButton.xaml
    /// </summary>
    public partial class ItemButton : UserControl
    {
        public ItemButton()
        {
            InitializeComponent();
        }

        public int ItemID 
        {
            get { return (int)GetValue(ItemIDProperty); }
            set { SetValue(ItemIDProperty, value); }
        }

        public static readonly DependencyProperty ItemIDProperty =
        DependencyProperty.Register("ItemID", typeof(int), typeof(ItemButton), null);
        public void Selected(object sender, RoutedEventArgs e)
        {
            var Details = new ItemDetails();
            (Details.DataContext as ItemDetailVM).Item = RepositorySwitcher.GetRepository().GetItems().Result[ItemID];
            Details.Show();
        }
    }
}
