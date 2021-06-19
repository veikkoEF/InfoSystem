using MyHome.Helpers;
using MyHome.Settings;
using RSSFeed;
using System;
using System.Collections.ObjectModel;


namespace MyHome.ViewModels
{
   
    public class RSSFeedControlViewModel : Observable
    {
        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private Uri imagePath;
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

        public ObservableCollection<FeedItem> Items { get; } = new ObservableCollection<FeedItem>();

        private async void  GetNews()
        {
            RSSFeedParser rSSFeedParser = new RSSFeedParser(ProgrammSettings.NewsFeed);
            FeedData result = await rSSFeedParser.GetData().ConfigureAwait(true);
            Title = result.Title;
            ImagePath = result.ImageUri;

            Items.Clear();
            foreach (var item in result.Items)
            {
                Items.Add(item);
            }


        }

        public RSSFeedControlViewModel()
        {
            GetNews();
        }
    }
}
