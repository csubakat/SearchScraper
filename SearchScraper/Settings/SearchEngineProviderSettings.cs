using System.IO;
using Microsoft.Extensions.Configuration;
using SearchScraper.Entitities;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Settings
{
    public class SearchEngineProviderSettings
    {
        private readonly IConfiguration _configuration;
        private readonly SearchEngine _searchEngine;

        private const string ParentSettingKey = "SearchEngineProviderSettings";
        private const string SearchStringParameterSettingKey = "SearchStringParameter";
        private const string NumberOfResultsParameterSettingKey = "NumberOfResultsParameter";
        private const string BaseUrlParameterSettingKey = "BaseUrl";

        public SearchEngineProviderSettings(SearchEngine searchEngine)
        {
            _searchEngine = searchEngine;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public SearchEngineProviderSetting GetSettings()
        {
            return new SearchEngineProviderSetting
            {
                BaseUrl = _configuration[$"{ParentSettingKey}:{_searchEngine.ToString()}:{BaseUrlParameterSettingKey}"],
                SearchStringParameter = _configuration[$"{ParentSettingKey}:{_searchEngine.ToString()}:{SearchStringParameterSettingKey}"],
                NumberOfResultsParameter = _configuration[$"{ParentSettingKey}:{_searchEngine.ToString()}:{NumberOfResultsParameterSettingKey}"],
            };
        }
    }
}
