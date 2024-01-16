using AutoMapper;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using ShopPriceComparer.Core.Factories;
using ShopPriceComparer.WebUI.Helpers;
using ShopPriceComparer.WebUI.Models;
using System.Diagnostics;
using System.Text;

namespace ShopPriceComparer.WebUI.Controllers
{

    /// <summary>
    /// The ProductsController class is responsible for handling requests related to products. It uses a ScraperFactory to scrape product data from different sources, an IMapper to map the scraped data to ProductViewModels, and an ILogger to log any events or errors that occur during these processes.
    /// </summary>
    /// <returns>
    /// Depending on the method, it may return a View, a PartialView with a list of matched products ordered by price, a PDF file, or an Error View Model.
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
        /// This is the asynchronous action method for the Index view. 
        /// It is responsible for handling HTTP GET requests to the home page of the application.
        /// </summary>
        /// <returns>
        /// An IActionResult that renders the Index view.
        /// </returns>
        public async Task<IActionResult> Index()
        {
            return View();
        }

        /// <summary>
        /// This asynchronous method is used to get product data from two different sources, MediaExpert and Morele. It scrapes the data from the respective URLs, maps the data to a ProductViewModel, compares the products from both sources based on a similarity threshold, and returns a partial view of the matched products ordered by price.
        /// </summary>
        /// <returns>
        /// Returns a Task resulting in an IActionResult that represents a PartialView of matched products ordered by price.
        /// </returns>
        public async Task<IActionResult> GetProductData()
        {
            const double similarityTreshold = 0.4;

            Uri mediaExpertUri = new Uri("http://www.mediaexpert.pl/smartfony-i-zegarki/smartfony/apple");
            var mediaExpertScraper = _scraper.CreateScraper(Core.Models.Shop.MediaExpert);
            var mediaExpertTask = mediaExpertScraper.ScrapAsync(mediaExpertUri);


            Uri moreleUri = new Uri("http://www.morele.net/kategoria/smartfony-280/,,,,,496,,,0,,,,");
            var moreleScraper = _scraper.CreateScraper(Core.Models.Shop.Morele);
            var moreleTask = moreleScraper.ScrapAsync(moreleUri);


            Task.WhenAll(mediaExpertTask, moreleTask);

            var mediaExpertProducts = mediaExpertTask.Result;
            var moreleProducts = moreleTask.Result;


            var mediaExpertProductVModel = _mapper.Map<List<ProductViewModel>>(mediaExpertProducts);
            var MoreleProductVModel = _mapper.Map<List<ProductViewModel>>(moreleProducts);

            var matchedProducts = ComparerHelper.CompareProducts(mediaExpertProductVModel, MoreleProductVModel, similarityTreshold);


            return PartialView("_ProductData", matchedProducts.OrderBy(x => x.Shop1Product.Price));
        }


        /// <summary>
        /// This method generates a PDF file from the provided HTML content.
        /// </summary>
        /// <param name="htmlContent">The HTML content to be converted into a PDF file.</param>
        /// <returns>
        /// Returns a PDF file converted from the provided HTML content.
        /// </returns>
        [HttpPost]
        public IActionResult GeneratePdf([FromBody] string htmlContent)
        {

            using MemoryStream stream = new MemoryStream();
            using MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent));
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);

            // Konwertuj zawartość HTML na plik PDF  
            HtmlConverter.ConvertToPdf(htmlStream, pdf);

            // Przesuń pozycję strumienia na początek  
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream.ToArray(), "application/pdf", "Porównanie_cen_smartfonów_Apple_wybranych_sklepów.pdf");
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
