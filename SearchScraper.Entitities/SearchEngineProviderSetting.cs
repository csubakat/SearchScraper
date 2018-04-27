namespace SearchScraper.Entitities
{
    public class SearchEngineProviderSetting
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string SearchStringParameter { get; set; }
        public string NumberOfResultsParameter { get; set; }
    }
}
