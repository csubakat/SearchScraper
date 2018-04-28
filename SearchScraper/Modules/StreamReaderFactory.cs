using System.IO;
using SearchScraper.Contracts;

namespace SearchScraper.Modules
{
    public class StreamReaderFactory : IStreamReaderFactory
    {
        public StreamReader Create(Stream stream)
        {
            return new StreamReader(stream);
        }
    }
}
