using System;
using System.Collections.Generic;
using SearchScraper.Entitities;
using SearchScraper.Entitities.Enums;
using SearchScraper.Settings;

namespace SearchScraper.Modules
{
    public class GoogleEngine : SearchEngineProvider
    {
        private readonly SearchEngineProviderSetting _settings;

        public GoogleEngine()
        {
            var settings = new SearchEngineProviderSettings(SearchEngine.Google);
            _settings = settings.GetSettings();
        }

        public override IDictionary<int, string> GetResults()
        {
            throw new NotImplementedException();
        }
    }
}
