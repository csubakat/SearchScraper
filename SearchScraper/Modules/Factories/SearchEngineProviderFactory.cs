using System;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;
using SearchScraper.Exceptions;
using SearchScraper.Modules.SearchEngines;

namespace SearchScraper.Modules.Factories
{
    public class SearchEngineProviderFactory : ISearchEngineProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchEngineProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchEngineProvider GetByName(SearchEngine searchEngine)
        {
            switch (searchEngine)
            {
                case SearchEngine.Google:
                    return (ISearchEngineProvider)_serviceProvider.GetService(typeof(GoogleEngine));
                default:
                    throw new InvalidSearchEngineException(searchEngine.ToString());
            }
        }
    }
}
