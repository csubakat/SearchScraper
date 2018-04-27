using System;
using System.Collections.Generic;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly ISearchEngineProviderFactory _searchEngineProviderFactory;

        private const int MaxNrOfResultsToBeQueries = 100;

        public ScrapingService(ISearchEngineProviderFactory searchEngineProviderFactory)
        {
            _searchEngineProviderFactory = searchEngineProviderFactory;
        }

        public IDictionary<int, string> GetSearchResults(SearchEngine searchEngine, string queryString, int nrOfResults)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentException("Search string cannot be empty");

            if (nrOfResults <= 0)
                throw new ArgumentException("Number of results to be queried must be greater than zero.");

            if (nrOfResults > MaxNrOfResultsToBeQueries)
                throw new ArgumentException("Cannot query more than 100 results at a time.");

            var provider = _searchEngineProviderFactory.GetByName(searchEngine);

            return provider.GetResults(queryString, nrOfResults);
        }
    }
}
