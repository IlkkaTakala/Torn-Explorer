using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveCharts;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using Newtonsoft.Json.Linq;
using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

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
        public SeriesCollection Series { get; }

        private async void GetProfile()
        {
            CurrentProfile = await RepositorySwitcher.GetRepository().GetProfile();

            double other = 0.0;
            foreach (var propertyInfo in CurrentProfile.Networth.GetType()
                                .GetProperties(
                                        BindingFlags.Public
                                        | BindingFlags.Instance))
            {
                if (propertyInfo.Name.Equals("Total") || propertyInfo.Name.Equals("Unpaidfees")) continue;

                double value = Convert.ToDouble(propertyInfo.GetValue(CurrentProfile.Networth, null));
                if (value / CurrentProfile.Networth.Total > 0.05)
                {
                    ISeriesView series = new PieSeries
                    {
                        Title = propertyInfo.Name,
                        Values = new ChartValues<double>(new List<double> { value }),
                        DataLabels = false,
                    };
                    Series.Add(series);
                }
                else
                {
                    other += value;
                }
            }
            ISeriesView otherSeries = new PieSeries
            {
                Title = "Other",
                Values = new ChartValues<double>(new List<double> { other }),
                DataLabels = false,
            };
            Series.Add(otherSeries);
        }

        private void OpenTorn()
        {
            Process.Start("https://www.torn.com/index.php");
        }

        public RelayCommand OpenLink { get; set; } 
        public RelayCommand Refresh { get; set; } 

        public string BackgroundImage { get
            {
                return "https://www.torn.com/token_shop.php?step=getImage&awardType=backdrops&title=2000x1083_burn&src=profiles";
            }
        }

        public ProfileVM() 
        {
            Series = new SeriesCollection();
            RepositorySwitcher.SetupRepository();
            OpenLink = new RelayCommand(OpenTorn);
            Refresh = new RelayCommand(async () => { 
                await RepositorySwitcher.GetRepository().RefreshProfile(); 
                OnPropertyChanged(nameof(CurrentProfile));
            });
            GetProfile();
        }
    }
}
