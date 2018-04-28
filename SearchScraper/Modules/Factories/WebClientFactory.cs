using SearchScraper.Contracts;
using SearchScraper.Modules.Clients;

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
