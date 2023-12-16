using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopPriceComparer.Core.Factories;
using ShopPriceComparer.WebUI.Models;
using System.Diagnostics;

namespace ShopPriceComparer.WebUI.Controllers
{
    /// <summary>
    /// The ProductsController class is responsible for handling the HTTP requests related to the products. 
    /// It uses a ScraperFactory to scrape product data from different sources, an IMapper to map the scraped data to the ProductViewModel, 
    /// and an ILogger to log any information, warnings, or errors that occur during the execution of its methods.
    /// </summary>
    /// <returns>
    /// The ProductsController returns an IActionResult that represents the result of its action methods. 
    /// The Index method returns a view of the products sorted by price. 
    /// The Error method returns a view of the ErrorViewModel that contains the ID of the current HTTP request.
    /// </returns>
    public class ProductsController : Controller
    {
        private readonly ScraperFactory _scraper;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor for the ProductsController class. Initializes the logger, scraper, and mapper.
        /// </summary>
        /// <param name="logger">Provides a mechanism for logging.</param>
        /// <param name="scraper">A factory for creating scraper instances.</param>
        /// <param name="mapper">A mapping interface to map between objects.</param>
        public ProductsController(ILogger<ProductsController> logger, ScraperFactory scraper, IMapper mapper)
        {
            _scraper = scraper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// This is the main action method for the home page. It scrapes data from the MediaExpert website, maps the data to a list of ProductViewModels, sorts the list by price, and then passes the list to the view.
        /// </summary>
        /// <returns>
        /// An IActionResult that renders the home page view with a list of ProductViewModels sorted by price.
        /// </returns>
        public async Task<IActionResult> Index()
        {
            #region MediaExpert 

            Uri mediaExpertUri = new Uri("http://www.mediaexpert.pl/smartfony-i-zegarki/smartfony");
            var mediaExpertScraper = _scraper.CreateScraper(Core.Models.Shop.MediaExpert);
            var mediaExpertProducts = await mediaExpertScraper.ScrapAsync(mediaExpertUri);
            var mediaExpertProductVModel = _mapper.Map<List<ProductViewModel>>(mediaExpertProducts);

            #endregion
            return View(mediaExpertProductVModel.OrderBy(x => x.Price));
        }

        /// <summary>
        /// This action method is used to handle errors and return an Error View Model with the current request ID.
        /// </summary>
        /// <returns>
        /// Returns a View displaying the ErrorViewModel which contains the ID of the current request or the trace identifier from the HttpContext if the current request ID is null.
        /// </returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
