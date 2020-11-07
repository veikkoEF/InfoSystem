using Hassie.NET.API.NewsAPI.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hassie.NET.API.NewsAPI.API.v2
{
    public class EverythingBuilder
    {
        private Category? mCategory;
        private Country? mCountry;
        private Language? mLanguage;
        private string mSearchQuery;
        private SortOrder? mSortOrder;
        private Source[] mSources;

        private UriBuilder mQuery;

        public EverythingBuilder WithCategoryQuery(Category category)
        {
            mCategory = category;
            return this;
        }

        public EverythingBuilder WithCountryQuery(Country country)
        {
            mCountry = country;
            return this;
        }

        public EverythingBuilder WithLanguageQuery(Language language)
        {
            mLanguage = language;
            return this;
        }

        public EverythingBuilder WithSearchQuery(string searchQuery)
        {
            mSearchQuery = searchQuery;
            return this;
        }

        public EverythingBuilder WithSortOrder(SortOrder sortOrder)
        {
            mSortOrder = sortOrder;
            return this;
        }

        public EverythingBuilder WithSourcesQuery(params Source[] sources)
        {
            mSources = sources;
            return this;
        }

        public EverythingBuilder Build()
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

            if (mSortOrder != null)
            {
                if (mSortOrder == SortOrder.PUBLISHED_AT)
                {
                    query.Add("sortBy", "publishedAt");
                }
                else
                {
                    query.Add("sortBy", mSortOrder.ToString().ToLower());
                }
            }

            if (mSources != null)
            {
                query.Add("sources", String.Join(",", mSources).Replace('_', '-'));
            }

            mQuery = new UriBuilder("https://newsapi.org/v2/everything")
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