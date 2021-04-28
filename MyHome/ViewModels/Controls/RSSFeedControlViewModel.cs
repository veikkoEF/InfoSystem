using MyHome.Helpers;
using RSSFeed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHome.ViewModels
{
   
    public class RSSFeedControlViewModel : Observable
    {

        public ObservableCollection<FeedItem> Items { get; set; } = new ObservableCollection<FeedItem>();

        private async void  GetNews()
        {
            //  RSSFeedParser rSSFeedParser = new RSSFeedParser("http://newsfeed.zeit.de/all");

            //  RSSFeedParser rSSFeedParser = new RSSFeedParser("https://www.tagesschau.de/xml/rss2/");

            //  RSSFeedParser rSSFeedParser = new RSSFeedParser("http://feeds.t-online.de/rss/erfurt");

            RSSFeedParser rSSFeedParser = new RSSFeedParser("https://www.n-tv.de/181.rss");

            //ObservableCollection<FeedItem> result = await rSSFeedParser.Parse();

            FeedData result = await rSSFeedParser.GetData();

            Items.Clear();
            foreach (var item in result.Items)
            {
                Items.Add(item);
            }


        }

        public RSSFeedControlViewModel()
        {
            GetNews();

            //FeedItem item = new FeedItem();
            //item.Title = "Test";
            //Items.Add(item);

        }
    }
}
