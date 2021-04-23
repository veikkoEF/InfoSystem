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
        SyndicationFeed feed = null;
        string rssFeed;

        public RSSFeedParser(string _rssFeed)
        {
            rssFeed = _rssFeed;
        }

        public List<SyndicationItem> Parse()
        {
            List<SyndicationItem> items = new List<SyndicationItem>();
            try
            {
                using (var reader = XmlReader.Create(rssFeed))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch { }
            if (feed != null)
            {
                foreach (var element in feed.Items)
                {
                    items.Add(element);
                }
            }
            return items;

        }

        public async Task<List<FeedItem>> Parse2()
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

            return items;
        }




    }

    
}










