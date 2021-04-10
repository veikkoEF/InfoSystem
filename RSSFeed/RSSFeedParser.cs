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
        public void Parse()
        {
            try
            {
                using (var reader = XmlReader.Create("https://visualstudiomagazine.com/rss-feeds/news.aspx"))
                {
                    feed = SyndicationFeed.Load(reader);
                }
            }
            catch { }
            if (feed != null)
            {
                foreach (var element in feed.Items)
                {
                    // Console.WriteLine($"Title: {element.Title.Text}");
                    // Console.WriteLine($"Summary: {element.Summary.Text}");
                }
            }

        }
    }
}


       

    


