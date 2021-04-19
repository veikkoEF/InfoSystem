using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ServiceModel.Syndication;


namespace RSSFeed
{
    public class RSSFeedParser
    {
        SyndicationFeed feed = null;
        string rssFeed;

        public RSSFeedParser(string _rssFeed)
        {
            rssFeed = _rssFeed;
        }

        public void Parse()
        {
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
                    var title = element.Title.Text;
                    var sum = element.Summary.Text;
                }
            }

        }
    }
}










