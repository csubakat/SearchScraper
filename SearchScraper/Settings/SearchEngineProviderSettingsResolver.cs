using System.IO;
using Microsoft.Extensions.Configuration;
using SearchScraper.Entitities;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Settings
{
    public class SearchEngineProviderSettingsResolver
    {
        private readonly IConfiguration _configuration;
        private readonly SearchEngine _searchEngine;

        private const string ParentSettingKey = "SearchEngineProviderSettings";
        private const string SearchStringParameterSettingKey = "SearchStringParameter";
        private const string NumberOfResultsParameterSettingKey = "NumberOfResultsParameter";
        private const string BaseUrlParameterSettingKey = "BaseUrl";

        public SearchEngineProviderSettingsResolver(SearchEngine searchEngine)
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
                BaseUrl = GetSettingValue(BaseUrlParameterSettingKey),
                SearchStringParameter = GetSettingValue(SearchStringParameterSettingKey),
                NumberOfResultsParameter = GetSettingValue(NumberOfResultsParameterSettingKey),
            };
        }

        private string GetSettingValue(string settingKey)
        {
            return _configuration[$"{ParentSettingKey}:{_searchEngine.ToString()}:{settingKey}"];
        }
    }
}
