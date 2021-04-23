using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using CodeHollow.FeedReader;

namespace RSSFeed
{
    // https://github.com/codehollow/FeedReader/blob/master/FeedReader/Feeds/Base/FeedItemEnclosure.cs
    
    public class RSSFeedParser
    {
        string rssFeed;
        public RSSFeedParser(string _rssFeed)
        {
            rssFeed = _rssFeed;
        }


        public async void Parse()
        {
            List<FeedItem> items = new List<FeedItem>();

            var feed = await FeedReader.ReadAsync(rssFeed);

            if (feed != null)
            {
                foreach (var element in feed.Items)
                {
                    items.Add(element);
                }
            }

            if (feed.Type == FeedType.Rss_2_0)
            {
                var rss20feed = (CodeHollow.FeedReader.Feeds.Rss20Feed)feed.SpecificFeed;

            }



 
        }

    }

    public class MyFeedItem
    {
        string Title { get; set; }
        string Description { get; set; }

    }
    
}










