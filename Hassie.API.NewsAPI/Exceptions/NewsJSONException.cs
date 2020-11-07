using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Exceptions
{
    public class NewsJSONException : Exception
    {
        public NewsJSONException(string message, JsonException inner) : base(message, inner) { }
    }
}
