using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SearchScraper.Contracts;
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

        public IActionResult Search(string queryString, int nrOfResults)
        {

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
