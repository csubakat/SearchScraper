using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using SearchScraper.Entitities.Enums;

namespace SearchScraper.Models
{
    public class SearchViewModel
    {
        public string SearchTerm { get; set; }
        public string StringToFind { get; set; }
        public string NumberOfResultsString { get; set; }
        public string SearchEngine { get; set; }

        public IEnumerable<SelectListItem> SearchEnginesOptions { get; set; }
        public IEnumerable<SelectListItem> NumberOfResultsOptions { get; set; }

        public IEnumerable<int> Occurences { get; set; }

        public SearchViewModel()
        {
            NumberOfResultsOptions = new List<SelectListItem>
            {
                new SelectListItem {Text = "25", Value = "25"},
                new SelectListItem {Text = "50", Value = "50"},
                new SelectListItem {Text = "75", Value = "75"},
                new SelectListItem {Text = "100", Value = "100", Selected = true}
            };

            SearchEnginesOptions =
                Enum.GetValues(typeof(SearchEngine)).Cast<SearchEngine>().Select(s => new SelectListItem
                {
                    Text = s.ToString(), Value = s.ToString()
                });
        }
    }
}
