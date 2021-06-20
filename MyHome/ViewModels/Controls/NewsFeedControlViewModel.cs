using MyHome.Helpers;
using MyHome.Settings;
using RSSFeed;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;

namespace MyHome.ViewModels
{
   
    public class NewsFeedControlViewModel : Observable
    {
        private readonly DispatcherTimer uiTimer = new DispatcherTimer();
        private int currentNumber;
        private Uri imagePath;
        private int index;
        private bool noDataAccess;
        private string title;
        public NewsFeedControlViewModel()
        {
            GetNewsAsync();
            currentNumber = 0;
            uiTimer.Interval = new TimeSpan(0, 0, 15);
            uiTimer.Start();
            uiTimer.Tick += UpdateNewsShowMessage;
        }

        public Uri ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }


        }

        // neue Nachricht
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                Set(ref index, value);
            }
        }

        public ObservableCollection<FeedItem> Items { get; } = new ObservableCollection<FeedItem>();

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

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public void StartUITimer()
        {
            uiTimer.Start();
        }

        public void StopUITimer()
        {
            uiTimer.Stop();
        }

        private async void GetNewsAsync()
        {
            RSSFeedParser rSSFeedParser = new RSSFeedParser(ProgrammSettings.NewsFeed);
            FeedData result = await rSSFeedParser.GetData().ConfigureAwait(true);
            if (result != null)
            {
                NoDataAccess = false;
                Title = result.Title;
                ImagePath = result.ImageUri;
                Items.Clear();
                foreach (var item in result.Items)
                {
                    Items.Add(item);
                }
            }
            else
                NoDataAccess = true;
            ShowNewsArticle(0);
        }

        private void ShowNewsArticle(int number = 0)
        {
            if ((number >= 0) && (number < Items.Count))
                Index = number;
        }

        private void UpdateNewsShowMessage(object sender, object e)
        {
            // Update News
            currentNumber++;
            if (currentNumber > 19)
                currentNumber = 0;
            ShowNewsArticle(currentNumber);
        }
    }
}
