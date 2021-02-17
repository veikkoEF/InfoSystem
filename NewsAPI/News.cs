using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Client;
using Hassie.NET.API.NewsAPI.Models;
using System.Threading.Tasks;

namespace NewsAPI
{
    public class News
    {
        private string apiKey;

        public News(string apiKey)
        {
            this.apiKey = apiKey;
        }


        public async Task<INewsArticles> GetNewsFromServiceAsync(Category category)
        {
            //  https://github.com/hassie-dash/NewsAPI.NET
            try
            {
                INewsClient newsClient = new ClientBuilder()
                {
                    ApiKey = apiKey
                }
                            .Build();
                INewsArticles newsArticles = await newsClient.GetTopHeadlines(new TopHeadlinesBuilder()
                    .WithCountryQuery(Country.DE)
                    .WithLanguageQuery(Language.DE)
                    .WithCategoryQuery(category)
                    .Build());
                return newsArticles;
            }
            catch
            {
                return null;
            }


        }

    }
}