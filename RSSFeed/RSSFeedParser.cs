using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;
using CodeHollow.FeedReader;
using CodeHollow.FeedReader.Feeds;

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
            List<MyFeedItem> items = new List<MyFeedItem>();

            Feed feed = await FeedReader.ReadAsync(rssFeed);

            if (feed != null)
            {
                if (feed.Type == FeedType.Rss_2_0)
                {
                    // Datenübernahme aus RSS20 Feed
                    Rss20Feed rss20feed = (Rss20Feed)feed.SpecificFeed;
                    foreach (var element in rss20feed.Items)
                    {
                        // 
                    }
                }
                else
                {
                    // Datenübernahme aus Feed
                    foreach (var element in feed.Items)
                    {
                         //items.Add(element);
                    }
                }
            }

             
            }

 
        }

    }

    public class MyFeedItem
    {
        string Title { get; set; }
        string Description { get; set; }

    }
    
}










