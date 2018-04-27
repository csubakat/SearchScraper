using System.Collections.Generic;
using System.Threading.Tasks;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Contracts
{
    public interface IScrapingService
    {
        Task<IDictionary<int, string>> GetSearchResults(SearchEngine searchEngine, string queryString, int nrOfResults);
    }
}
