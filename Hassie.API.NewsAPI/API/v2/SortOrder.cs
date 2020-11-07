using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.API.v2
{
    public enum SortOrder
    {
        /// <summary>
        /// Articles from popular sources and publishers come first.
        /// </summary>
        POPULARITY,
        /// <summary>
        /// Newest articles come first.
        /// </summary>
        PUBLISHED_AT,
        /// <summary>
        /// Articles related more closely to the search query come first.
        /// </summary>
        RELEVANCY
    }
}
