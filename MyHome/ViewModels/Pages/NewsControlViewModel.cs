﻿#pragma warning disable CA2007 // Aufruf von "ConfigureAwait" für erwarteten Task erwägen
using Hassie.NET.API.NewsAPI.Models;
using MyHome.Helpers;
using MyHome.Settings;
using System;
using Windows.UI.Xaml;
using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Client;

namespace MyHome.ViewModels
{
    public class NewsViewModel : Observable
    {
        private string description;
        private string headline;
        private Uri myPath;
        private INewsArticles newsArticles;
        private string source;
        private string content;
        private int currentNumber;
        private readonly DispatcherTimer uiTimer = new DispatcherTimer(); // neue Nachricht
        private readonly DispatcherTimer updateTimer = new DispatcherTimer(); // neuer Download der Nachrichten

        public void StartUITimer()
        {
            uiTimer.Start();
        }

        public void StopUITimer()
        {
            uiTimer.Stop();
        }

        public NewsViewModel()
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
            //  https://github.com/hassie-dash/NewsAPI.NET
            try
            {
                INewsClient newsClient = new ClientBuilder()
                {
                    ApiKey = ProgrammSettings.NewsAPIKey
                }
                .Build();
                newsArticles = await newsClient.GetTopHeadlines(new TopHeadlinesBuilder()
                    .WithCountryQuery(Country.DE)
                    .WithLanguageQuery(Language.DE)
                    .WithCategoryQuery(ProgrammSettings.NewsCategory)
                    .Build());
            }
            catch
            {

            }

            ShowNewsArticle(currentNumber);
        }

        private void ShowNewsArticle(int number = 0)
        {
            if (newsArticles != null)
            {
                try
                {
                    NoDataAccess = false;
                    Headline = newsArticles[number].Title;
                    MyPath = new Uri(newsArticles[number].ImageURL);
                    Description = newsArticles[number].Description;
                    Source = newsArticles[number].SourceName;
                    Content = newsArticles[number].Content;
                }
                catch
                {
                    NoDataAccess = true;
                }
            }
            else
            {
                NoDataAccess = true;
            }
        }
    }
}
