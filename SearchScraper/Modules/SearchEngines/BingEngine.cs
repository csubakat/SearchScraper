using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules.SearchEngines
{
    public class BingEngine : SearchEngineProvider
    {
        public BingEngine(SearchEngineProviderSetting settings, IWebClientFactory webClientFactory) : base(settings, webClientFactory)
        {
        }

        public override Task<IEnumerable<int>> GetOccurencesAsync(string searchTerm, string stringToFind, int nrOfResults)
        {
            throw new NotImplementedException();
        }
    }
}
