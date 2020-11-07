using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.Models
{
    internal class NewsSource : INewsSource
    {
        private readonly string mCategory;
        private readonly string mCountry;
        private readonly string mDescription;
        private readonly string mID;
        private readonly string mLanguage;
        private readonly string mName;
        private readonly string mURL;

        public NewsSource(string category, string country, string description, string id, string language, string name, string url)
        {
            mCategory = category;
            mCountry = country;
            mDescription = description;
            mID = id;
            mLanguage = language;
            mName = name;
            mURL = url;
        }

        public string Category => mCategory;

        public string Country => mCountry;

        public string Description => mDescription;

        public string ID => mID;

        public string Language => mLanguage;

        public string Name => mName;

        public string URL => mURL;
    }
}