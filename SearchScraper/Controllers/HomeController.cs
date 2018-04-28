using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SearchScraper.Contracts;
using SearchScraper.Entitities.Enums;
using SearchScraper.Exceptions;
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

        [HttpPost]
        public async Task<IActionResult> Search(string engine, string q, string f, int n = 100)
        {
            var invalidSearchEngineMessage = $"{engine} is not supported yet.";

            if (!Enum.TryParse(engine, true, out SearchEngine searchEngineEnum))
                return BadRequest(invalidSearchEngineMessage);

            try
            {
                var results = await _scrapingService.GetSearchResults(searchEngineEnum, q, f, n).ConfigureAwait(false);
                return View("Index", results);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidSearchEngineException ex)
            {
                return BadRequest(invalidSearchEngineMessage);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

