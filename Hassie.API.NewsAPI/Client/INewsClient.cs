using Hassie.NET.API.NewsAPI.API.v2;
using Hassie.NET.API.NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hassie.NET.API.NewsAPI.Client
{
    public interface INewsClient
    {
        /// <summary>
        /// Returns all headlines using the provided query.
        /// </summary>
        /// <param name="query">The query to be provided with the request.</param>
        Task<INewsArticles> GetEverything(EverythingBuilder query);

        /// <summary>
        /// Returns top headlines using the provided query.
        /// </summary>
        /// <param name="query">The query to be provided with the request.</param>
        Task<INewsArticles> GetTopHeadlines(TopHeadlinesBuilder query);

        /// <summary>
        /// Returns all news sources.
        /// </summary>
        Task<INewsSources> GetNewsSources();

        /// <summary>
        /// Returns news sources using the provided query.
        /// </summary>
        /// <param name="query">The query to be provided with the request.</param>
        Task<INewsSources> GetNewsSources(NewsSourcesBuilder query);
    }
}