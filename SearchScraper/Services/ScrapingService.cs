﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<int>> GetSearchResultsAsync(SearchEngine searchEngine, string searchTerm, string stringToFind, int nrOfResults)
        {
            if (string.IsNullOrEmpty(searchTerm) || string.IsNullOrEmpty(stringToFind))
                throw new ArgumentException("Search term and string to find cannot be empty.");

            if (nrOfResults <= 0)
                throw new ArgumentException("Number of results to be queried must be greater than 0.");

            if (nrOfResults > MaxNrOfResultsToBeQueries)
                throw new ArgumentException("Cannot query more than 100 results at a time.");

            var provider = _searchEngineProviderFactory.GetByName(searchEngine);
            var occurences = (await provider.GetOccurencesAsync(searchTerm, stringToFind, nrOfResults)).ToList();

            return !occurences.Any() ? new List<int> { 0 } : occurences;
        }
    }
}
