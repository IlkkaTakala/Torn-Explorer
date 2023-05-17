using CommunityToolkit.Mvvm.ComponentModel;
using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public class ProfileVM : ObservableObject
    {
        private Profile _profile;

        public Profile CurrentProfile
        {
            get { return _profile; }
            set { _profile = value; OnPropertyChanged("CurrentProfile"); }
        }

        private async void GetProfile()
        {
            CurrentProfile = await RepositorySwitcher.GetRepository().GetProfile();
        }

        public ProfileVM() 
        {
            RepositorySwitcher.SetupRepository();
            GetProfile();
        }
    }
}
