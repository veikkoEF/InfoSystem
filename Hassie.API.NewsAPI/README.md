NewsAPI.NET
===========
[![Build Status](https://travis-ci.org/hassie-dash/NewsAPI.NET.svg?branch=master)](https://travis-ci.org/hassie-dash/NewsAPI.NET)

NewsAPI.NET is a asynchronous API wrapper around News API. It is based on their new v2 API and is written in .NET Standard 1.1.

NuGet
-----
This library is available on NuGet, under the package name Hassie.NET.API.NewsAPI. The current version is 2.2.1.

Example usage
-------------
```cs
INewsClient newsClient = new ClientBuilder()
    {
        ApiKey = "Your API Key";
    }
    .Build();

INewsArticles newsArticles = await newsClient.GetTopHeadlines(new TopHeadlinesBuilder()
	.WithCountryQuery(Country.GB)
	.WithLanguageQuery(Language.EN)
	.WithSourcesQuery(Source.BBC_NEWS, Source.BBC_SPORT)
	.Build());

foreach (INewsArticle article in newsArticles)
{
	Console.WriteLine(article.Author);
	Console.WriteLine(article.Description);
	Console.WriteLine(article.ImageURL);
	Console.WriteLine(article.PublishTime);
	Console.WriteLine(article.SourceName);
	Console.WriteLine(article.Title);
	Console.WriteLine(article.URL);
}
```

License
-------
Copyright ©2018 Hassie.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
