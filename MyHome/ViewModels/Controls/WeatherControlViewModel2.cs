#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using MyHome.Helpers;
using MyHome.Settings;
using OpenWeatherForecast;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyHome.ViewModels
{
    public class WeatherControlViewModel2 : Observable
    {
        private string icon3H;
        private string icon6H;
        private string icon9H;
        private string iconCurrent;
        private string nameOfDay1;
        private string nameOfDay2;
        private string nameOfDay3;
        private string sunrise;
        private string sunset;
        private string temp3h;
        private string temp6h;
        private string temp9h;
        private string tempCurrent;
        private string time3h;
        private string time6h;
        private string time9h;
        private string timeCurrent;
        private double tempMinDay1;
        private double tempMaxDay1;
        private double tempMinDay2;
        private double tempMaxDay2;
        private double tempMinDay3;
        private double tempMaxDay3;



        public WeatherControlViewModel2()
        {
            GetWeatherDateFromServiceAsync();
        }

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

        public string IconCurrent
        {
            get => iconCurrent;
            set => Set(ref iconCurrent, value);
        }

        public double TempMinDay1
        {
            get => tempMinDay1;
            set => Set(ref tempMinDay1, value);
        }

        public double TempMinDay2
        {
            get => tempMinDay2;
            set => Set(ref tempMinDay2, value);
        }

        public double TempMinDay3
        {
            get => tempMinDay3;
            set => Set(ref tempMinDay3, value);
        }

        public double TempMaxDay1
        {
            get => tempMaxDay1;
            set => Set(ref tempMaxDay1, value);
        }

        public double TempMaxDay2
        {
            get => tempMaxDay2;
            set => Set(ref tempMaxDay2, value);
        }

        public double TempMaxDay3
        {
            get => tempMaxDay3;
            set => Set(ref tempMaxDay3, value);
        }

        public string Icon3H
        {
            get => icon3H;
            set => Set(ref icon3H, value);
        }

        public string Icon6H
        {
            get => icon6H;
            set => Set(ref icon6H, value);
        }

        public string Icon9H
        {
            get => icon9H;
            set => Set(ref icon9H, value);
        }

        public string NameOfDay1
        {
            get => nameOfDay1;
            set => Set(ref nameOfDay1, value);
        }

        public string NameOfDay2
        {
            get => nameOfDay2;
            set => Set(ref nameOfDay2, value);
        }
        public string NameOfDay3
        {
            get => nameOfDay3;
            set => Set(ref nameOfDay3, value);
        }
        public string Sunrise
        {
            get => sunrise;
            set => Set(ref sunrise, value);
        }

        public string Sunset
        {
            get => sunset;
            set => Set(ref sunset, value);
        }


        public string Temp3h
        {
            get => temp3h;
            set => Set(ref temp3h, value);
        }

        public string Temp6h
        {
            get => temp6h;
            set => Set(ref temp6h, value);
        }

        public string Temp9h
        {
            get => temp9h;
            set => Set(ref temp9h, value);
        }

        public string TempCurrent
        {
            get => tempCurrent;
            set => Set(ref tempCurrent, value);
        }

        public string Time3h
        {
            get => time3h;
            set => Set(ref time3h, value);
        }

        public string Time6h
        {
            get => time6h;
            set => Set(ref time6h, value);
        }

        public string Time9h
        {
            get => time9h;
            set => Set(ref time9h, value);
        }

        public string TimeCurrent
        {
            get => timeCurrent;
            set => Set(ref timeCurrent, value);
        }
        private async void GetWeatherDateFromServiceAsync()
        {
            var location = ProgrammSettings.Location;
            OpenWeatherMapProxyForecast openWeatherMapProxyForecast = new OpenWeatherMapProxyForecast(ProgrammSettings.OpenWeatherMapKey);
            RootObject myWeather = await openWeatherMapProxyForecast.GetWeather(location);
            if (myWeather != null)
            {
                // aktuelles Wetter
                TempCurrent = Math.Round(myWeather.list[0].main.temp, 0).ToString() + " °C";
                TimeCurrent = DateTimeOffset.FromUnixTimeSeconds(myWeather.list[0].dt).DateTime.ToLocalTime().ToString("HH:mm");
                IconCurrent = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[0].weather[0].icon);

                // Sonnenaufgang/- untergang (sunrise/ sunset)
                DateTime _sunrise = DateTimeOffset.FromUnixTimeSeconds(long.Parse(myWeather.city.sunrise)).DateTime.ToLocalTime();
                Sunrise = _sunrise.ToString("HH:mm");
                DateTime _sunset = DateTimeOffset.FromUnixTimeSeconds(long.Parse(myWeather.city.sunset)).DateTime.ToLocalTime();
                Sunset = _sunset.ToString("HH:mm");

                // + 3h
                Temp3h = Math.Round(myWeather.list[1].main.temp, 0).ToString() + " °C";
                Time3h = DateTimeOffset.FromUnixTimeSeconds(myWeather.list[1].dt).DateTime.ToLocalTime().ToString("HH:mm");
                Icon3H = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[1].weather[0].icon);

                // + 6h
                Temp6h = Math.Round(myWeather.list[2].main.temp, 0).ToString() + " °C";
                Time6h = DateTimeOffset.FromUnixTimeSeconds(myWeather.list[2].dt).DateTime.ToLocalTime().ToString("HH:mm");
                Icon6H = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[2].weather[0].icon);

                // + 9h
                Temp9h = Math.Round(myWeather.list[3].main.temp, 0).ToString() + " °C";
                Time9h = DateTimeOffset.FromUnixTimeSeconds(myWeather.list[3].dt).DateTime.ToLocalTime().ToString("HH:mm");
                Icon9H = string.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.list[3].weather[0].icon);

                // Day 1
                DateTime day1 = DateTime.Parse(myWeather.list[0].dt_txt, new CultureInfo("de-DE"));
                NameOfDay1 = day1.ToString("dddd", new CultureInfo("de-DE"));

                
                


                // Day 2
                DateTime day2 = day1.AddDays(1);
                NameOfDay2 = day2.ToString("dddd", new CultureInfo("de-DE"));

                // Day 3
                DateTime day3 = day1.AddDays(2);
                NameOfDay3 = day3.ToString("dddd", new CultureInfo("de-DE"));

                // Min und Max Temperatur
                List<double> tempListDay1 = new List<double>();
                List<double> tempListDay2 = new List<double>();
                List<double> tempListDay3 = new List<double>();
                for (int i = 0; i < myWeather.list.Count; i++)
                {
                    string nameOfDay=DateTime.Parse(myWeather.list[i].dt_txt, new CultureInfo("de-DE")).ToString("dddd", new CultureInfo("de-DE"));
                    if (NameOfDay1 == nameOfDay)
                    {
                        double temp = Math.Round(myWeather.list[i].main.temp, 0);
                        tempListDay1.Add(temp);
                    }
                    else if (NameOfDay2 == nameOfDay)
                    {
                        double temp = Math.Round(myWeather.list[i].main.temp, 0);
                        tempListDay2.Add(temp);
                    }
                    else if (NameOfDay3 == nameOfDay)
                    {
                        double temp = Math.Round(myWeather.list[i].main.temp, 0);
                        tempListDay3.Add(temp);
                    }
                }
                TempMinDay1 = tempListDay1.Min();
                TempMaxDay1 = tempListDay1.Max();

                TempMinDay2 = tempListDay2.Min();
                TempMaxDay2 = tempListDay2.Max();

                TempMinDay3 = tempListDay3.Min();
                TempMaxDay3 = tempListDay3.Max();
            }
        }
    }
}
