using System;

namespace SearchScraper.Exceptions
{
    public class InvalidSearchEngineException : Exception
    {
        public InvalidSearchEngineException(string searchEngine) : base($"{searchEngine} is not supported yet.")
        {
        }
    }
}
