using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules
{
    public abstract class SearchEngineProvider : ISearchEngineProvider
    {
        protected SearchEngineProvider(SearchEngineProviderSetting settings) { }

        protected bool IsValid(int nrOfResults, string searchTerm)
        {
            return nrOfResults > 0 && !string.IsNullOrEmpty(searchTerm);
        }

        protected Uri CreateQueryUrl(SearchEngineProviderSetting setting, string queryTerm, int nrOfResults)
        {
            return new Uri($"{setting.BaseUrl}{setting.SearchStringParameter}{queryTerm}&{setting.NumberOfResultsParameter}{nrOfResults}", UriKind.Absolute);
        }

        public abstract Task<IEnumerable<int>> GetResults(string searchTerm, string stringToFind, int nrOfResults);
    }
}
