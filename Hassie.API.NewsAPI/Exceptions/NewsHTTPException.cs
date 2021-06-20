using System;
using System.Net.Http;

namespace Hassie.NET.API.NewsAPI.Exceptions
{
    public class NewsHTTPException : Exception
    {
        public NewsHTTPException(string message) : base(message)
        {
        }

        public NewsHTTPException(string message, HttpRequestException inner) : base(message, inner)
        {
        }
    }
}