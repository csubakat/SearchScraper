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

        public async Task<IActionResult> Search(string engine, string q, string f, int n = 100)
        {
            if (!Enum.TryParse(engine, true, out SearchEngine searchEngineEnum))
                return BadRequest($"{engine} is not a supported engine.");


            var results = await _scrapingService.GetSearchResults(searchEngineEnum, q, f, n).ConfigureAwait(false);
            return View("Index", results);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

