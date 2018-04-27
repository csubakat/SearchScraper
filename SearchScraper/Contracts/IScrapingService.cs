using System.Collections.Generic;
using System.Threading.Tasks;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Contracts
{
    public interface IScrapingService
    {
        Task<IEnumerable<int>> GetSearchResults(SearchEngine searchEngine, string searchTerm, string stringToFind, int nrOfResults);
    }
}
