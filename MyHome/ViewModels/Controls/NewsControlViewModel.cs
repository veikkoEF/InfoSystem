#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using Hassie.NET.API.NewsAPI.Models;
using MyHome.Helpers;
using MyHome.Settings;
using NewsAPI;
using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace MyHome.ViewModels
{
    public class NewsControlViewModel : Observable
    {
        private string description;
        private string headline;
        //private string myPath = string.Empty;
        private Uri myPath;
        private INewsArticles newsArticles;
        private string source;
        private string content;
        private int currentNumber;
        private DispatcherTimer uiTimer = new DispatcherTimer(); // neue Nachricht
        private DispatcherTimer updateTimer = new DispatcherTimer(); // neuer Download der Nachrichten

        public NewsControlViewModel()
        {
            GetNewsAsync();
            currentNumber = 0;
            uiTimer.Interval = new TimeSpan(0, 0, ProgrammSettings.TimeInSecondToChangeNewsMessage);
            uiTimer.Start();
            uiTimer.Tick += UpdateNewsShowMessage;
            updateTimer.Interval = new TimeSpan(1, 0, 0);
            updateTimer.Start();
            updateTimer.Tick += GetNewMessageFromCloud;
        }

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

        private void GetNewMessageFromCloud(object sender, object e)
        {
            GetNewsAsync();
            
        }

        private void UpdateNewsShowMessage(object sender, object e)
        {
            // Update News
            currentNumber++;
            if (currentNumber > 19)
                currentNumber = 0;
            ShowNewsArticle(currentNumber);
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Headline
        {
            get => headline;
            set
            {
                headline = value;
                OnPropertyChanged(nameof(Headline));
            }
        }

        public Uri MyPath
        {
            get { return myPath; }
            set
            {
                myPath = value;
                OnPropertyChanged(nameof(MyPath));
            }
        }

        

        public string Source
        {
            get { return source; }
            set
            {
                source = value;
                OnPropertyChanged(nameof(Source));
            }
        }

        public string Content
        {
            get { return content; }
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
            }
        }

        private async void GetNewsAsync()
        {
            News news = new News(ProgrammSettings.NewsAPIKey);
            newsArticles = await news.GetNewsFromServiceAsync(ProgrammSettings.NewsCategory);
            ShowNewsArticle(currentNumber);
        }

        private void ShowNewsArticle(int number = 0)
        {
            if (newsArticles != null)
            {
                NoDataAccess = false;
                Headline = newsArticles[number].Title;
                MyPath = new Uri(newsArticles[number].ImageURL);
                Description = newsArticles[number].Description;
                Source = newsArticles[number].SourceName;
                Content = newsArticles[number].Content;
            }
            else
            {
                NoDataAccess = true;
            }
                
        }
    }
}
