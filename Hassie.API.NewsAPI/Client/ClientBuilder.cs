using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Client
{
    public class ClientBuilder
    {
        private string apiKey;

        public string ApiKey
        {
            set { apiKey = value; }
        }

        public INewsClient Build()
        {
            return new NewsClient(apiKey);
        }
    }
}