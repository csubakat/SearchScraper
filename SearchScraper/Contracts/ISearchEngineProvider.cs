using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProvider
    {
        Task<IDictionary<int, string>> GetResults(string searchString, int nrOfResults);
    }
}
