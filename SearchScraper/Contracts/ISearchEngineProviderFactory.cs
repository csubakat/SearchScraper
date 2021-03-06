﻿using SearchScraper.Entitities.Enums;

namespace SearchScraper.Contracts
{
    public interface ISearchEngineProviderFactory
    {
        ISearchEngineProvider GetByName(SearchEngine searchEngine);
    }
}
