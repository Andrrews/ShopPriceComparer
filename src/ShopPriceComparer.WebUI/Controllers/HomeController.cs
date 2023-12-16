using Microsoft.AspNetCore.Mvc;
using ShopPriceComparer.Core.Factories;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.WebUI.Models;
using System.Diagnostics;

namespace ShopPriceComparer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ScraperFactory _scraper;

        public HomeController(ILogger<HomeController> logger, ScraperFactory scraper)
        {
            _logger = logger;
            _scraper = scraper;
        }

        public async Task<IActionResult> Index()
        {
            var mediaExpertShop = ShopPriceComparer.Core.Models.Shop.MediaExpert;
            var MediaExpertScraper = _scraper.CreateScraper(mediaExpertShop);
            var prod = await MediaExpertScraper.ScrapAsync(new Uri("http://www.mediaexpert.pl/smartfony-i-zegarki/smartfony"));
            return View();
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
