using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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

        public override IDictionary<int, string> GetResults(string queryTerm, int nrOfResults)
        {
            string html;
            using (var webClient = new WebClient())
            {
                html = webClient.DownloadString(CreateQueryUrl(_settings, queryTerm, nrOfResults));
            }

            var regex = new Regex("<div class=\"g\">(.*?)</div>");
            var matches = regex.Matches(html).ToList();

            var occurences = (from m in matches
                    where m.ToString().Contains(queryTerm)
                    select new KeyValuePair<int, string>()).ToDictionary(x => x.Key, x => x.Value);

            return occurences;
        }
    }
}
