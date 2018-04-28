using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProvider
    {
        Task<IEnumerable<int>> GetOccurences(string searchTerm, string stringToFind, int nrOfResults);
    }
}
