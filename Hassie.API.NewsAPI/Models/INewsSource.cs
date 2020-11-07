using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    public interface INewsSource
    {
        string Category { get; }

        string Country { get; }

        string Description { get; }

        string ID { get; }

        string Language { get; }

        string Name { get; }

        string URL { get; }
    }
}