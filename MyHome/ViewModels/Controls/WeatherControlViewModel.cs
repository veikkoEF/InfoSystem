#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen

using MyHome.Helpers;
using MyHome.Settings;
using OpenWeatherForecast;
using System;
using System.Globalization;

namespace MyHome.ViewModels
{
    public class WeatherControlViewModel : Observable
    {
        private double tempDay1;
        private double tempDay2;
        private double tempDay3;
        private string iconDay1;
        private string iconDay2;
        private string iconDay3;
        private DateTime day1;
        private DateTime day2;
        private DateTime day3;
        private string timeOfTemps;
        private string nameOfDay2;
        private string nameOfDay3;
        private bool noDataAccess;

        public bool NoDataAccess
        {
            get
            {
                return noDataAccess;
            }
            set
            {
                noDataAccess = value;
                OnPropertyChanged(nameof(NoDataAccess));
            }
        }

        public WeatherControlViewModel()
        {
            GetWeatherDateFromServiceAsync();
        }

        /// <summary>
        /// Zugriff auf statische Klasse Settings
        /// </summary>
        public string Location
        {
            get
            {
                return ProgrammSettings.Location;
            }
            set
            {
                ProgrammSettings.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }

        public double TempDay1
        {
            get => tempDay1;
            set
            {
                tempDay1 = value;
                OnPropertyChanged(nameof(TempDay1));
            }
        }

        public double TempDay2
        {
            get => tempDay2;
            set
            {
                tempDay2 = value;
                OnPropertyChanged(nameof(TempDay2));
            }
        }

        public double TempDay3
        {
            get => tempDay3;
            set
            {
                tempDay3 = value;
                OnPropertyChanged(nameof(TempDay3));
            }
        }

        public string TimeOfTemps
        {
            get => timeOfTemps;
            set
            {
                timeOfTemps = value;
                OnPropertyChanged(nameof(TimeOfTemps));
            }
        }

        public string NameOfDay2
        {
            get => nameOfDay2;
            set
            {
                nameOfDay2 = value;
                OnPropertyChanged(nameof(NameOfDay2));
            }
        }

        public string NameOfDay3
        {
            get => nameOfDay3;
            set
            {
                nameOfDay3 = value;
                OnPropertyChanged(nameof(NameOfDay3));
            }
        }

        public string IconDay1
        {
            get => iconDay1;
            set
            {
                iconDay1 = value;
                OnPropertyChanged(nameof(IconDay1));
            }
        }

        public string IconDay2
        {
            get => iconDay2;
            set
            {
                iconDay2 = value;
                OnPropertyChanged(nameof(IconDay2));
            }
        }

        public string IconDay3
        {
            get => iconDay3;
            set
            {
                iconDay3 = value;
                OnPropertyChanged(nameof(IconDay3));
            }
        }

        private async void GetWeatherDateFromServiceAsync()
        {
            var location = ProgrammSettings.Location;
            OpenWeatherMapProxyForecast openWeatherMapProxyForecast = new OpenWeatherMapProxyForecast(ProgrammSettings.OpenWeatherMapKey);
            RootObject myWeather = await openWeatherMapProxyForecast.GetWeather(location);
            if (myWeather != null)
            {
                NoDataAccess = false;
                // Tag 1
                TempDay1 = Math.Round(myWeather.list[0].main.temp, 0);
                day1 = DateTime.Parse(myWeather.list[0].dt_txt, new CultureInfo("de-DE"));
                IconDay1 = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[0].weather[0].icon);
                // Tag 2
                TempDay2 = Math.Round(myWeather.list[8].main.temp, 0);
                day2 = DateTime.Parse(myWeather.list[8].dt_txt, new CultureInfo("de-DE"));
                IconDay2 = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[8].weather[0].icon);
                NameOfDay2 = day2.ToString("dddd", new CultureInfo("de-DE"));
                // Tag 3
                TempDay3 = Math.Round(myWeather.list[16].main.temp, 0);
                day3 = DateTime.Parse(myWeather.list[16].dt_txt, new CultureInfo("de-DE"));
                IconDay3 = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[16].weather[0].icon);
                NameOfDay3 = day3.ToString("dddd", new CultureInfo("de-DE"));
                // allgemeine Zeitangabe
                TimeOfTemps = "jeweils um " + day1.TimeOfDay.Hours + " Uhr";
            }
            else
                NoDataAccess = true;
        }
    }
}