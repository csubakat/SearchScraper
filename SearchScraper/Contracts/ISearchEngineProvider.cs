using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProvider
    {
        Task<IEnumerable<int>> GetOccurencesAsync(string searchTerm, string stringToFind, int nrOfResults);
    }
}
