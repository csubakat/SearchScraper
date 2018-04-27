using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public override async Task<IDictionary<int, string>> GetResults(string searchString, int nrOfResults)
        {
            string html;
            using (var webClient = new WebClient())
            {
                html = await webClient.DownloadStringTaskAsync(CreateQueryUrl(_settings, searchString, nrOfResults));
            }

            var regex = new Regex($"<cite class=\"iUh30\">({searchString})</cite>", RegexOptions.IgnoreCase);
            var matches = regex.Matches(html).ToList();

            var indexes = matches.Select((x, i) => new { i, x })
                .Select(x => x.i + 1)
                .ToList();

            var occurences = (from m in matches
                    where m.ToString().Contains(searchString)
                    select new KeyValuePair<int, string>()).ToDictionary(x => x.Key, x => x.Value);

            return occurences;
        }
    }
}
