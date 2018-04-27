using System;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Modules
{
    public class SearchEngineProviderFactory : ISearchEngineProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchEngineProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISearchEngineProvider Resolve(SearchEngine searchEngine)
        {
            switch (searchEngine)
            {
                case SearchEngine.Google:
                    return (ISearchEngineProvider)_serviceProvider.GetService(typeof(GoogleEngine));
                default:
                    return null;
            }
        }
    }
}
