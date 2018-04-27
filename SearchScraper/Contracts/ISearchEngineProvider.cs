using System.Collections.Generic;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProvider
    {
        bool IsValid(int nrOfResults, string searchTerm);
        IDictionary<int, string> GetResults();
    }
}
