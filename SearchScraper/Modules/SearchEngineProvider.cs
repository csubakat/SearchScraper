using System.Collections.Generic;
using SearchScraper.Contracts;

namespace SearchScraper.Modules
{
    public abstract class SearchEngineProvider : ISearchEngineProvider
    {
        public bool IsValid(int nrOfResults, string searchTerm)
        {
            return nrOfResults > 0 && !string.IsNullOrEmpty(searchTerm);
        }

        public abstract IDictionary<int, string> GetResults();
    }
}
