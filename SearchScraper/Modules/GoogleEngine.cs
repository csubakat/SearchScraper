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

        public override async Task<IEnumerable<int>> GetResults(string searchTerm, string stringToFind, int nrOfResults)
        {
            string html;
            using (var webClient = new WebClient())
            {
                html = await webClient.DownloadStringTaskAsync(CreateQueryUrl(_settings, searchTerm, nrOfResults));
            }

            var resultNodeRegex = new Regex($"<div class=\"g\">(.*?)</div>", RegexOptions.IgnoreCase);

            var matches = resultNodeRegex.Matches(html).ToList();
            //TODO understand and rewrite to something more readable
            var occurences = matches.Select((x, i) => new { i, x })
                .Where(x => x.ToString().Contains(stringToFind))
                .Select(x => x.i + 1)
                .ToList();

            return occurences;
        }
    }
}
