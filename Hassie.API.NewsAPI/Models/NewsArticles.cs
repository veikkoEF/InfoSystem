using Hassie.NET.API.NewsAPI.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    internal class NewsArticles : List<INewsArticle>, INewsArticles
    {
        public NewsArticles(JObject json)
        {
            try
            {
                // Get array with articles.
                JArray array = (JArray)json["articles"];

                // Extract each article and add to list.
                foreach (JObject article in array)
                {
                    string author = (string)article["author"];
                    string description = (string)article["description"];
                    string imageURL = (string)article["urlToImage"];
                    DateTimeOffset publishTime = DateTimeOffset.Parse(article["publishedAt"].ToString());
                    string sourceName = (string)article["source"]["name"];
                    string title = (string)article["title"];
                    string url = (string)article["url"];
                    string content = (string)article["content"];
                    NewsArticle newsArticle = new NewsArticle(author, description, imageURL, publishTime, sourceName, title, url, content);
                    Add(newsArticle);
                }
            }
            catch (JsonException e)
            {
                throw new NewsJSONException("News API JSON Exception - Failed to extract JSON", e);
            }
        }
    }
}
