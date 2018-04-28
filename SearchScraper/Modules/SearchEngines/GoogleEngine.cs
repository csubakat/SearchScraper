using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules.SearchEngines
{
    public class GoogleEngine : SearchEngineProvider
    {
        private readonly SearchEngineProviderSetting _settings;
        private readonly IWebClientFactory _webClientFactory;

        public GoogleEngine(SearchEngineProviderSetting settings, IWebClientFactory webClientFactory) : base(settings, webClientFactory)
        {
            _settings = settings;
            _webClientFactory = webClientFactory;
        }

        public override async Task<IEnumerable<int>> GetOccurencesAsync(string searchTerm, string stringToFind, int nrOfResults)
        {
            string html;

            using (var webClient = _webClientFactory.Create())
            {
                html = await webClient.DownloadStringTaskAsync(CreateQueryUrl(_settings, searchTerm, nrOfResults));
            }

            var resultNodeRegex = new Regex("<div class=\"g\">(.*?)</div>", RegexOptions.IgnoreCase);

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
