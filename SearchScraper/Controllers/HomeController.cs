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
            var model = new SearchViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            try
            {
                if (!Enum.TryParse(model.SearchEngine, true, out SearchEngine searchEngineEnum))
                    throw new InvalidSearchEngineException(model.SearchEngine);

                if (!int.TryParse(model.NumberOfResultsString, out var numberOfResults))
                    throw new ArgumentException("Number of results must be an integer.");

                model.Occurences = await _scrapingService.GetSearchResultsAsync(searchEngineEnum, model.SearchTerm, model.StringToFind, numberOfResults).ConfigureAwait(false);
                return View("Index", model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidSearchEngineException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

