using Hassie.NET.API.NewsAPI.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.API.v2
{
    public class TopHeadlinesBuilder
    {
        private Category? mCategory;
        private Country? mCountry;
        private Language? mLanguage;
        private string mSearchQuery;
        private Source[] mSources;

        private UriBuilder mQuery;

        public TopHeadlinesBuilder WithCategoryQuery(Category category)
        {
            mCategory = category;
            return this;
        }

        public TopHeadlinesBuilder WithCountryQuery(Country country)
        {
            mCountry = country;
            return this;
        }

        public TopHeadlinesBuilder WithLanguageQuery(Language language)
        {
            mLanguage = language;
            return this;
        }

        public TopHeadlinesBuilder WithSearchQuery(string searchQuery)
        {
            mSearchQuery = searchQuery;
            return this;
        }

        public TopHeadlinesBuilder WithSourcesQuery(params Source[] sources)
        {
            mSources = sources;
            return this;
        }

        public TopHeadlinesBuilder Build()
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

            if (mSearchQuery != null)
            {
                query.Add("q", mSearchQuery.Replace(" ", "%20"));
            }

            if (mSources != null)
            {
                query.Add("sources", String.Join(",", mSources).Replace('_', '-'));
            }

            mQuery = new UriBuilder("https://newsapi.org/v2/top-headlines")
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