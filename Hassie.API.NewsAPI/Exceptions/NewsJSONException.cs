using Newtonsoft.Json;
using System;

namespace Hassie.NET.API.NewsAPI.Exceptions
{
    public class NewsJSONException : Exception
    {
        public NewsJSONException(string message, JsonException inner) : base(message, inner)
        {
        }
    }
}