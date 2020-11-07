using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Exceptions;
using Hassie.NET.API.NewsAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hassie.NET.API.NewsAPI.Client
{
    internal class NewsClient : INewsClient
    {
        private readonly string apiKey;

        public NewsClient(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task<INewsArticles> GetEverything(EverythingBuilder query)
        {
            return new NewsArticles(await GetResponse(String.Concat(query.ToString(), "&apiKey=", apiKey))); 
        }

        public async Task<INewsArticles> GetTopHeadlines(TopHeadlinesBuilder query)
        {
            return new NewsArticles(await GetResponse(String.Concat(query.ToString(), "&apiKey=", apiKey)));
        }

        public async Task<INewsSources> GetNewsSources()
        {
            return new NewsSources(await GetResponse(String.Concat(new NewsSourcesBuilder().Build().ToString(), "?apiKey=", apiKey)));
        }

        public async Task<INewsSources> GetNewsSources(NewsSourcesBuilder query)
        {
            return new NewsSources(await GetResponse(String.Concat(query.ToString(), "&apiKey=", apiKey)));
        }

        private async Task<JObject> GetResponse(string query)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Get response from API.
                    HttpResponseMessage response = await httpClient.GetAsync(query);

                    // Check status code.
                    if (response.IsSuccessStatusCode)
                    {
                        // Success, parse json.
                        return JObject.Parse(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        // Parse error json and throw exception.
                        JObject json = JObject.Parse(await response.Content.ReadAsStringAsync());
                        throw new NewsHTTPException($"News API HTTP Exception - {json["code"]}: {json["message"]}");
                    }
                }
            }
            catch (HttpRequestException e1)
            {
                throw new NewsHTTPException("News API HTTP Exception - Failed to get response", e1);
            }
            catch (JsonException e2)
            {
                throw new NewsJSONException("News API JSON Exception - Failed to parse response", e2);
            }
        }
    }
}