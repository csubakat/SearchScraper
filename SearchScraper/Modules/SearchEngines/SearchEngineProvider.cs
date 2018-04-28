using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules.SearchEngines
{
    public abstract class SearchEngineProvider : ISearchEngineProvider
    {
        protected SearchEngineProvider(SearchEngineProviderSetting settings) { }

        protected Uri CreateQueryUri(SearchEngineProviderSetting setting, string queryTerm, int nrOfResults)
        {
            return new Uri($"{setting.BaseUrl}{setting.SearchStringParameter}{Uri.EscapeDataString(queryTerm)}&{setting.NumberOfResultsParameter}{nrOfResults}", UriKind.Absolute);
        }

        public abstract Task<IEnumerable<int>> GetOccurencesAsync(string searchTerm, string stringToFind, int nrOfResults);
    }
}
