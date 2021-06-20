using System;
using System.Collections.ObjectModel;

namespace RSSFeed
{
    public class FeedData
    {
        public ObservableCollection<FeedItem> Items { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri ImageUri { get; set; }

        public FeedData()
        {
            Items = new ObservableCollection<FeedItem>();
        }
    }
}