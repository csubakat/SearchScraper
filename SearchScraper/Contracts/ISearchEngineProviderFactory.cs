using SearchScraper.Modules;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProviderFactory<T> where T : SearchEngineProvider
    {
        T Create();
    }
}
