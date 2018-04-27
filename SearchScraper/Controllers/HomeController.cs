using System;
using System.Diagnostics;
using System.Threading.Tasks;
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

        public async Task<IActionResult> Search(string searchEngine, string searchString, int nrOfResults = 100)
        {
            if (!Enum.TryParse(searchEngine, true, out SearchEngine searchEngineEnum))
                return BadRequest($"{searchEngine} is not a supported engine.");


            var results = await _scrapingService.GetSearchResults(searchEngineEnum, searchString, nrOfResults).ConfigureAwait(false);

            return View("Index", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
