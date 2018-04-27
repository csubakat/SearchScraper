using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;
using SearchScraper.Models;

namespace SearchScraper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IScrapingService _scrapingService;

        public HomeController(IScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search(string searchEngine, string searchString, int nrOfResults)
        {
            if (!Enum.TryParse(searchEngine, true, out SearchEngine searchEngineEnum))
                return BadRequest($"{searchEngine} is not a supported engine.");

            var results = _scrapingService.GetSearchResults(searchEngineEnum, searchString, nrOfResults);

            return View("Index", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
