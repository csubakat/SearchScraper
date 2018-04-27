using System.Collections.Generic;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProvider
    {
        IDictionary<int, string> GetResults(string queryTerm, int nrOfResults);
    }
}
