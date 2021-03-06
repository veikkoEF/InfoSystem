﻿using CodeHollow.FeedReader;
using System;
using System.Threading.Tasks;

namespace RSSFeed
{
    // https://github.com/codehollow/FeedReader/blob/master/FeedReader/Feeds/Base/FeedItemEnclosure.cs
    // https://validator.w3.org/feed/docs/rss2.html

    public class RSSFeedParser
    {
        private string rssFeed;

        public RSSFeedParser(string _rssFeed)
        {
            rssFeed = _rssFeed;
        }

        public async Task<FeedData> GetData()
        {
            try
            {
                FeedData feedData = new FeedData();
                Feed feed = await FeedReader.ReadAsync(rssFeed);
                feedData.Title = feed.Title;
                feedData.ImageUri = new Uri(feed.ImageUrl);

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
                        feedData.Items.Add(feedItem);
                    }
                }
                return feedData;
            }
            catch
            {
                return null;
            }
        }
    }
}