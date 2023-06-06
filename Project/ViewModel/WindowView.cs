using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Controls;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ViewModel
{
    public class WindowView : ObservableObject
    {
        public MainPage MainPage { get; set; }

        public Settings SettingsPage { get; set; }

        public Page CurrentPage { get; set; }

        public RelayCommand SwitchPageCommand { get; set; }
        public RelayCommand Refresh { get; set; }
        public RelayCommand OpenProfile { get; set; }

        public string APIText { get; set; } 

        private void OpenProf()
        {
            var page = new Profile();
            page.Show();
        }

        private async void RefreshData()
        {
            List<Task> tasks = new List<Task>
            {
                RepositorySwitcher.GetRepository().RefreshItems(),
                RepositorySwitcher.GetRepository().RefreshWars(),
                RepositorySwitcher.GetRepository().RefreshItemTypes()
            };
            await Task.WhenAll(tasks);
        }
        public void SwitchPage()
        {
            if (CurrentPage is MainPage)
            {
                CurrentPage = SettingsPage;
                OnPropertyChanged("CurrentPage");
                APIText = "Return";
                OnPropertyChanged(nameof(APIText));
            }
            else if (CurrentPage is Settings)
            {
                CurrentPage = MainPage;
                OnPropertyChanged("CurrentPage");
                APIText = "Set API key";
                OnPropertyChanged(nameof(APIText));
            }
        }
        public WindowView() 
        {
            RepositorySwitcher.SetupRepository();
            MainPage = new MainPage();
            SettingsPage = new Settings();
            (SettingsPage.DataContext as SettingsVM).GoBack = new RelayCommand(SwitchPage);
            CurrentPage = SettingsPage;
            SwitchPageCommand = new RelayCommand(SwitchPage);
			Refresh = new RelayCommand(RefreshData);
            OpenProfile = new RelayCommand(OpenProf);
            APIText = "Return";
            OnPropertyChanged(nameof(APIText));
        }
    }
}
