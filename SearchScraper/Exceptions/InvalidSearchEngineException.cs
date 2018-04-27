using System;

namespace SearchScraper.Exceptions
{
    public class InvalidSearchEngineException : Exception
    {
        public InvalidSearchEngineException(string message) : base(message) { }
    }
}
