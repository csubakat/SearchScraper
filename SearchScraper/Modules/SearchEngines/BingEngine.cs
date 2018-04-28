using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SearchScraper.Contracts;
using SearchScraper.Entitities;

namespace SearchScraper.Modules.SearchEngines
{
    public class BingEngine : SearchEngineProvider
    {
        private readonly SearchEngineProviderSetting _settings;
        private readonly string _apiAccessKey;
        private readonly IStreamReaderFactory _streamReaderFactory;

        private const string AccessKeyHeader = "Ocp-Apim-Subscription-Key";
        private const int MaxNrOfResults = 50;

        public BingEngine(SearchEngineProviderSetting settings, string apiAccessKey, IStreamReaderFactory streamReaderFactory) : base(settings)
        {
            _settings = settings;
            _apiAccessKey = apiAccessKey;
            _streamReaderFactory = streamReaderFactory;
        }

        public override async Task<IEnumerable<int>> GetOccurencesAsync(string searchTerm, string stringToFind, int nrOfResults)
        {
            var offset = 0;
            var nextBatchCount = 0;

            if (nrOfResults > MaxNrOfResults)
            {
                // Bing has a maximum of 50 results returned for each API call
                offset = MaxNrOfResults;
                nextBatchCount = nrOfResults - MaxNrOfResults;
                nrOfResults = MaxNrOfResults;
            }

            var bingSearchResults = Deserialise(await GetSearchResultAsync(searchTerm, nrOfResults));

            if (nextBatchCount > 0)
            {
                var nextBatchResult = await GetSearchResultAsync(searchTerm, nextBatchCount, offset);
                bingSearchResults.AddRange(Deserialise(nextBatchResult));
            }

            var occurences = new List<int>();

            foreach (var result in bingSearchResults)
            {
                if (result.Snippet.Contains(stringToFind) || result.Url.Contains(stringToFind))
                {
                    var index = bingSearchResults.IndexOf(result) + 1;
                    occurences.Add(index);
                }
            }

            return occurences;
        }

        private async Task<string> GetSearchResultAsync(string searchTerm, int nrOfResults, int offset = 0)
        {
            var queryUri = CreateQueryUri(_settings, searchTerm, nrOfResults);

            var request = WebRequest.Create(queryUri);
            request.Headers[AccessKeyHeader] = _apiAccessKey;

            var response = (HttpWebResponse)await request.GetResponseAsync();

            if (response.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException();

            string json;
            using (var reader = _streamReaderFactory.Create(response.GetResponseStream()))
            {
                json = await reader.ReadToEndAsync();
            }

            return json;
        }

        private List<BingSearchResult> Deserialise(string json)
        {
            var jObject = JObject.Parse(json);

            var results = jObject["webPages"]["value"].Children().ToList();

            var resultList = new List<BingSearchResult>();
            foreach (var result in results)
            {
                resultList.Add(result.ToObject<BingSearchResult>());
            }

            return resultList;
        }
    }
}
