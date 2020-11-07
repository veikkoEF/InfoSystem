using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    public interface INewsArticle
    {
        string Author { get; }

        string Description { get; }

        string ImageURL { get; }

        DateTimeOffset PublishTime { get; }

        string SourceName { get; }

        string Title { get; }

        string URL { get; }

        string Content { get; }
    }
}
