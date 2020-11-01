#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using Hassie.NET.API.NewsAPI.API.v2;
using System;
using System.Collections.Generic;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Microsoft.Toolkit.Uwp.Helpers;
using Windows.Foundation.Diagnostics;

namespace MyHome.Settings
{

    public static class ProgrammSettings
    {
        // Section Home
        public static string Message { get; set; } = "Family";
        public static int ShowDurationHome { get; set; } = 5;

        // Section Weather
        public static int ShowDurationWeather { get; set; } = 20;
        public static WeatherMode WeatherModus { get; set; } = WeatherMode.TreeDaysComfort;
        public static string Location { get; set; } = "Erfurt";
        public static string OpenWeatherMapKey { get; set; }

        // Section News
        public static int ShowDurationNews { get; set; } = 60;
        public static Category NewsCategory { get; set; } = Category.GENERAL;
        public static int TimeInSecondToChangeNewsMessage { get; set; } = 15;
        public static string NewsAPIKey { get; set; }

        // Section Pictures
        public static int ShowDurationPictures { get; set; } = 60;
        public static PictureSeletionMode PictureSeletionMode { get; set; } = PictureSeletionMode.ByRandom;
        public static ArtOfPicturePresentation ArtOfPicturePresentation { get; set; } = ArtOfPicturePresentation.SinglePicture;
        public static string DropBoxAppToken { get; set; }
        public static int TimeInSecondToChangePicture { get; set; } = 5;
        public static string NameOfCurrentDir { get; set; }

        // Section Clock
        public static int ShowDurationClock { get; set; } = 20;
        public static bool AutomaticColor { get; set; } = true;

        private const string fileNameOfKeys = "keys.txt";
        private static UISettings _uiSettings = new UISettings();
        public static Windows.UI.Color ForegroundColor { get; set; } = _uiSettings.GetColorValue(UIColorType.Background);
        public static Windows.UI.Color BackgroundColor { get; set; } = _uiSettings.GetColorValue(UIColorType.Accent);
        public static bool ShowSeconds { get; set; }
        public static ClockMode ClockModus { get; set; } = ClockMode.Analog;

        // Section Map
        public static int ShowDurationMap { get; set; } = 35;
        public static string MapsAPIKey { get; set; }

        // Section App
        public static bool AutoChangeContentSections { get; set; } = true;
        public static bool HomeIsActiv { get; set; } = true;
        public static bool WeatherIsActiv { get; set; } = true;
        public static bool NewsIsActiv { get; set; } = true;
        public static bool PictureIsActiv { get; set; } = true;
        public static bool ClockIsActiv { get; set; } = true;
        public static bool MapIsActiv { get; set; } = true;


        public static void Save()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            // Section Home
            localSettings.Values["HomeMessage"] = Message;
            localSettings.Values["ShowDurationHome"] = ShowDurationHome;

            // Section Weather
            localSettings.Values["ShowDurationWeather"] = ShowDurationWeather;
            localSettings.Values["WeatherModus"] = (int)WeatherModus;
            localSettings.Values["Location"] = Location;
            localSettings.Values["OpenWeatherMapKey"] = OpenWeatherMapKey;

            // Section New
            localSettings.Values["ShowDurationNews"] = ShowDurationNews;
            localSettings.Values["NewsCategory"] = (int)NewsCategory;
            localSettings.Values["TimeInSecondToChangePicture"] = TimeInSecondToChangePicture;
            localSettings.Values["NewsAPIKey"] = NewsAPIKey;

            // Section Pictures
            localSettings.Values["ShowDurationPictures"] = ShowDurationPictures;
            localSettings.Values["ArtOfPicturePresentation"] = (int)ArtOfPicturePresentation;
            localSettings.Values["DropBoxAppToken"] = DropBoxAppToken;
            localSettings.Values["TimeInSecondToChangeNewsMessage"] = TimeInSecondToChangeNewsMessage;
            localSettings.Values["NameOfCurrentDir"] = NameOfCurrentDir;

            // Section Clock
            localSettings.Values["ShowDurationClock"] = ShowDurationClock;
            localSettings.Values["ShowSeconds"] = ShowSeconds;
            localSettings.Values["AutomaticColor"] = AutomaticColor;
            localSettings.Values["BackgroundColor"] = BackgroundColor.ToString();
            localSettings.Values["ForegroundColor"] = ForegroundColor.ToString();
            localSettings.Values["ClockModus"] = (int)ClockModus;

            // Section Map
            localSettings.Values["MapsAPIKey"] = MapsAPIKey;

            // Section App
            localSettings.Values["AutoChangeContentSections"] = AutoChangeContentSections;
            localSettings.Values["HomeIsActiv"] = HomeIsActiv;
            localSettings.Values["WeatherIsActiv"] = WeatherIsActiv;
            localSettings.Values["NewsIsActiv"] = NewsIsActiv;
            localSettings.Values["PictureIsActiv"] = PictureIsActiv;
            localSettings.Values["ClockIsActiv"] = ClockIsActiv;
            localSettings.Values["MapIsActiv"] = MapIsActiv;



        }

        public static void Load()
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

                // Section Home
                Message = localSettings.Values["HomeMessage"].ToString();
                ShowDurationHome = (int)localSettings.Values["ShowDurationHome"];

                // Section Weather
                ShowDurationWeather = (int)localSettings.Values["ShowDurationWeather"];
                WeatherModus = (WeatherMode)localSettings.Values["WeatherModus"];
                Location = localSettings.Values["Location"].ToString();
                if (localSettings.Values["OpenWeatherMapKey"] == null)
                {
                    OpenWeatherMapKey = "";
                }
                else
                {
                    OpenWeatherMapKey = localSettings.Values["OpenWeatherMapKey"].ToString();
                }

                // Section New
                ShowDurationNews = (int)localSettings.Values["ShowDurationNews"];
                NewsCategory = (Category)localSettings.Values["NewsCategory"];
                TimeInSecondToChangeNewsMessage = (int)localSettings.Values["TimeInSecondToChangeNewsMessage"];
                if (localSettings.Values["NewsAPIKey"] == null)
                {
                    NewsAPIKey = "";
                }
                else
                {
                    NewsAPIKey = localSettings.Values["NewsAPIKey"].ToString();
                }

                
                // Section Pictures
                ShowDurationPictures = (int)localSettings.Values["ShowDurationPictures"];
                ArtOfPicturePresentation = (ArtOfPicturePresentation)localSettings.Values["ArtOfPicturePresentation"];

                if (localSettings.Values["DropBoxAppToken"] == null)
                {
                    DropBoxAppToken = "";
                }
                else
                {
                    DropBoxAppToken = localSettings.Values["DropBoxAppToken"].ToString();
                }
                TimeInSecondToChangePicture = (int)localSettings.Values["TimeInSecondToChangePicture"];
                NameOfCurrentDir=localSettings.Values["NameOfCurrentDir"].ToString();

                // Section Clock
                ShowDurationClock = (int)localSettings.Values["ShowDurationClock"];
                ShowSeconds = (bool)localSettings.Values["ShowSeconds"];
                AutomaticColor = (bool)localSettings.Values["AutomaticColor"];
                BackgroundColor = ColorHelper.ToColor((string)localSettings.Values["BackgroundColor"]);
                ForegroundColor = ColorHelper.ToColor((string)localSettings.Values["ForegroundColor"]);
                ClockModus = (ClockMode)localSettings.Values["ClockModus"];

                // Section Map
                if (localSettings.Values["MapsAPIKey"] == null)
                {
                    MapsAPIKey = "";
                }
                else
                {
                    MapsAPIKey = localSettings.Values["MapsAPIKey"].ToString();
                }


                // Section App
                AutoChangeContentSections = (bool)localSettings.Values["AutoChangeContentSections"];

                HomeIsActiv = (bool)localSettings.Values["HomeIsActiv"];

                WeatherIsActiv = (bool)localSettings.Values["WeatherIsActiv"];
                if (OpenWeatherMapKey == "")
                    WeatherIsActiv = false;

                NewsIsActiv = (bool)localSettings.Values["NewsIsActiv"];
                if (NewsAPIKey == "")
                    NewsIsActiv = false;

                PictureIsActiv = (bool)localSettings.Values["PictureIsActiv"];
                if (DropBoxAppToken == "")
                    PictureIsActiv = false;

                ClockIsActiv = (bool)localSettings.Values["ClockIsActiv"];

                MapIsActiv= (bool)localSettings.Values["MapIsActiv"];
                if (MapsAPIKey == "")
                    MapIsActiv = false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Save the keys in a file an save on app folder
        /// </summary>
        public static async void SaveKeysAsync()
        {
            // https://stackoverflow.com/questions/41870125/uwp-save-file-in-documents-and-pictures-library
            // aktuellen App-Ordner ermitteln
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            // Datei generieren
            StorageFile file = await storageFolder.CreateFileAsync(fileNameOfKeys, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, DropBoxAppToken + "\r\n");
            var keys = new List<string> { OpenWeatherMapKey, NewsAPIKey, MapsAPIKey};
            await FileIO.AppendLinesAsync(file, keys);
        }

        /// <summary>
        /// Load the keys from a file from app folder
        /// </summary>
        public static async void LoadKeysAsync()
        {
            // https://docs.microsoft.com/de-de/windows/uwp/get-started/fileio-learning-track
            // aktueller Ordner
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            bool result = await StorageFileHelper.FileExistsAsync(storageFolder, fileNameOfKeys, false);
            if (result == true)
            {
                StorageFile file = await storageFolder.GetFileAsync(fileNameOfKeys);
                var keys = await FileIO.ReadLinesAsync(file);
                if (keys.Count > 0)
                    DropBoxAppToken = keys[0];
                if (keys.Count > 1)
                    OpenWeatherMapKey = keys[1];
                if (keys.Count > 2)
                    NewsAPIKey = keys[2];
                if (keys.Count > 3)
                    MapsAPIKey = keys[3];
            }
        }
    }
}
