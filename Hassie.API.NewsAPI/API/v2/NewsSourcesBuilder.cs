using Hassie.NET.API.NewsAPI.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.API.v2
{
    public class NewsSourcesBuilder
    {
        private Category? mCategory;
        private Country? mCountry;
        private Language? mLanguage;
        private UriBuilder mQuery;

        public NewsSourcesBuilder WithCategoryQuery(Category category)
        {
            mCategory = category;
            return this;
        }

        public NewsSourcesBuilder WithCountryQuery(Country country)
        {
            mCountry = country;
            return this;
        }

        public NewsSourcesBuilder WithLanguageQuery(Language language)
        {
            mLanguage = language;
            return this;
        }

        public NewsSourcesBuilder Build()
        {
            RequestQueryBuilder query = new RequestQueryBuilder();

            if (mCategory != null)
            {
                query.Add("category", mCategory.ToString().ToLower());
            }

            if (mCountry != null)
            {
                query.Add("country", mCountry.ToString().ToLower());
            }

            if (mLanguage != null)
            {
                query.Add("language", mLanguage.ToString().ToLower());
            }

            mQuery = new UriBuilder("https://newsapi.org/v2/sources")
            {
                Query = query.ToString()
            };

            return this;
        }

        public override string ToString()
        {
            return mQuery.ToString();
        }
    }
}
