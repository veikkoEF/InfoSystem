using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;


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
    }
}










