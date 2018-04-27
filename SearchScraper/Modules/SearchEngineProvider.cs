using System.Collections.Generic;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules
{
    public abstract class SearchEngineProvider : ISearchEngineProvider
    {
        private bool IsValid(int nrOfResults, string searchTerm)
        {
            return nrOfResults > 0 && !string.IsNullOrEmpty(searchTerm);
        }

        private string CreateQueryUrl(SearchEngineProviderSetting setting, string queryTerm, int nrOfResults)
        {
            return $"{setting.BaseUrl}{queryTerm}&{setting.NumberOfResultsParameter}{nrOfResults}";
        }

        public abstract IDictionary<int, string> GetResults();
    }
}
