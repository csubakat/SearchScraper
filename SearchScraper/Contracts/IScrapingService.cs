using System.Collections.Generic;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Contracts
{
    public interface IScrapingService
    {
        IDictionary<int, string> GetSearchResults(SearchEngine searchEngine, string queryString, int nrOfResults);
    }
}
