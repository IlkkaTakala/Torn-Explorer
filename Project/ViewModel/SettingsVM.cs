using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.ViewModel
{
    public class SettingsVM : ObservableObject
    {
        public RelayCommand<string> SetAPI { get; private set; }
        public RelayCommand SkipAPI { get; private set; }
        public RelayCommand GoBack { get; set; }
        public SettingsVM() 
        {
            SetAPI = new RelayCommand<string>((s) =>
            {
                if (s != null && s.Length == 16)
                {
                    RepositorySwitcher.SetAPI(s);
                    GoBack.Execute(this);
                }
            });

            SkipAPI = new RelayCommand(() => { 
                RepositorySwitcher.SetAPI(null);
                GoBack.Execute(this);
            });
        }
    }
}
