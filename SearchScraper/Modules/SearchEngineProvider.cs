using System.Collections.Generic;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules
{
    public abstract class SearchEngineProvider : ISearchEngineProvider
    {
        protected bool IsValid(int nrOfResults, string searchTerm)
        {
            return nrOfResults > 0 && !string.IsNullOrEmpty(searchTerm);
        }

        protected string CreateQueryUrl(SearchEngineProviderSetting setting, string queryTerm, int nrOfResults)
        {
            return $"{setting.BaseUrl}{queryTerm}&{setting.NumberOfResultsParameter}{nrOfResults}";
        }

        public abstract IDictionary<int, string> GetResults(string queryTerm, int nrOfResults);
    }
}
