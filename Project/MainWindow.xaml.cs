using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
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

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var Items = LocalRepository.GetItems();

            lstItems.ItemsSource = Items;
        }

        private void AddAPI(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Add API key here:", "Add key");
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {

        }
    }
}
