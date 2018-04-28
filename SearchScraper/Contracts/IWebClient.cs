using System;
using System.Threading.Tasks;

namespace SearchScraper.Contracts
{
    public interface IWebClient : IDisposable
    {
        Task<string> DownloadStringTaskAsync(Uri address);
    }
}
