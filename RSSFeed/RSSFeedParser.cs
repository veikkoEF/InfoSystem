using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;
using System.Collections.ObjectModel;

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


        public async Task<ObservableCollection<FeedItem>> Parse()
        {
            ObservableCollection<FeedItem> items = new ObservableCollection<FeedItem>();

            Feed feed = await FeedReader.ReadAsync(rssFeed);

            if (feed != null)
            {
                
                foreach (var element in feed.Items)
                {
                    FeedItem feedItem = new FeedItem();
                    feedItem.Title = element.Title;
                    feedItem.Description = element.Description;
                    feedItem.Link = element.Link;
                    if (feed.Type == FeedType.Rss_2_0)
                    {

                    }

                    items.Add(feedItem);

                }
            }
            return items;
        }
    }



    public class FeedItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

    }
    
}











