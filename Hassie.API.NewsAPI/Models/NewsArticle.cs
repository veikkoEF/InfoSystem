using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    internal class NewsArticle : INewsArticle
    {
        private readonly string author;
        private readonly string description;
        private readonly string imageURL;
        private readonly DateTimeOffset publishTime;
        private readonly string sourceName;
        private readonly string title;
        private readonly string url;
        private readonly string content;

        public NewsArticle(string author, string description, string imageURL, DateTimeOffset publishTime, 
            string sourceName, string title, string url, string content)
        {
            this.author = author;
            this.description = description;
            this.imageURL = imageURL;
            this.publishTime = publishTime;
            this.sourceName = sourceName;
            this.title = title;
            this.url = url;
            this.content = content;
        }

        public string Author => author;

        public string Description => description;

        public string ImageURL => imageURL;

        public DateTimeOffset PublishTime => publishTime;

        public string SourceName => sourceName;

        public string Title => title;

        public string URL => url;

        public string Content => content;
    }
}
