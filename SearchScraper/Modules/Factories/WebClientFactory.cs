using SearchScraper.Contracts;

namespace SearchScraper.Modules.Factories
{
    public class WebClientFactory : IWebClientFactory
    {
        public IWebClient Create()
        {
            return new SystemWebClient();
        }
    }
}
