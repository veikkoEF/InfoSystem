using Hassie.NET.API.NewsAPI.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    internal class NewsSources : List<INewsSource>, INewsSources
    {
        public NewsSources(JObject json)
        {
            try
            {
                // Get array with sources.
                JArray array = (JArray)json["sources"];

                // Extract each source and add to list.
                foreach (JObject source in array)
                {
                    string category = (string)source["category"];
                    string country = (string)source["country"];
                    string description = (string)source["description"];
                    string id = (string)source["id"];
                    string language = (string)source["language"];
                    string name = (string)source["name"];
                    string url = (string)source["url"];

                    INewsSource newsSource = new NewsSource(category, country, description, id, language, name, url);

                    Add(newsSource);
                }
            }
            catch (JsonException e)
            {
                throw new NewsJSONException("News API JSON Exception - Failed to extract JSON", e);
            }
        }
    }
}