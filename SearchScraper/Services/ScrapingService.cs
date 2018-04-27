using System;
using System.Collections.Generic;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Services
{
    public class ScrapingService : IScrapingService
    {
        private readonly ISearchEngineProviderFactory _providerFactory;

        public ScrapingService(ISearchEngineProviderFactory providerFactory)
        {
            _providerFactory = providerFactory;
        }

        public IDictionary<int, string> GetResults(SearchEngine searchEngine, string queryString, int nrOfResults)
        {
            if (string.IsNullOrEmpty(queryString))
                throw new ArgumentException("Search string cannot be empty");

            if (nrOfResults <= 0)
                throw new ArgumentException("Number of results must be greater than zero.");

            var provider = _providerFactory.GetByName(searchEngine);

            return provider.GetResults(queryString, nrOfResults);
        }
    }
}
