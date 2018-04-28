using System.IO;

namespace SearchScraper.Contracts
{
    public interface IStreamReaderFactory
    {
        StreamReader Create(Stream stream);
    }
}
