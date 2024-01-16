using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Services
{
    /// <summary>
    /// This class represents a scraper for the Morele shop. It implements the IScraper interface.
    /// </summary>
    /// <returns>
    /// The ScrapAsync method returns a Task that represents a list of products from the Morele shop.
    /// </returns>
    public class MoreleScraper : IScraper
    {
        /// <summary>
        /// Gets the shop for which this scraper is designed.
        /// </summary>
        public Shop Shop => Shop.Morele;
        private readonly IWebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoreleScraper"/> class with the provided WebDriver.
        /// </summary>
        /// <param name="driver">The WebDriver to be used for scraping.</param>
        public MoreleScraper(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// Scrapes product data asynchronously from the specified URI.
        /// </summary>
        /// <param name="uri">The URI of the website to scrape.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter contains a list of scraped products.</returns>
        public Task<List<Product>> ScrapAsync(Uri uri)
        {
            List<Product> products = new List<Product>();
            try
            {

                _driver.Navigate().GoToUrl(uri);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(300);
                var title = _driver.Title;
                //WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
                var lastPageButton = _driver.FindElement(By.ClassName("pagination-btn-nolink-anchor"));
                var lastPageButtonVal = lastPageButton.Text;
                int.TryParse(lastPageButtonVal, out int totalPages);

                for (int currentPage = 1; currentPage <= totalPages; currentPage++)
                {

                    string updatedUrl = uri.GetLeftPart(UriPartial.Path) + $"/{currentPage}/";
                    _driver.Navigate().GoToUrl(updatedUrl);
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(300);

                    IReadOnlyList<IWebElement> offerBox = _driver.FindElements(By.ClassName("cat-product"));
                    foreach (IWebElement e in offerBox)
                    {
                        try
                        {
                            decimal price = 0;
                            Product product = new Product();

                            var priceColl = e.FindElements(By.ClassName("price-new"));
                            if (priceColl.Count > 0)
                            {
                                var decVal = priceColl.First().Text.Replace("zł", "");
                                decVal = decVal.Replace("\r\n", ",");
                                decVal = new string(decVal.Where(c => !char.IsWhiteSpace(c)).ToArray()).Replace('\u202F', ' ');
                                if (decVal.Length > 0 && decVal.EndsWith(","))
                                {
                                    decVal = decVal.Remove(decVal.Length - 1);
                                }

                                decimal.TryParse(decVal, out price);
                                product.Price = price;
                            }
                            product.ShopName = Shop.ToString();
                            var nameColl = e.FindElements(By.ClassName("productLink"));
                            if (nameColl.Count > 0)
                            {
                                var nameVal = nameColl.Last().Text;
                                product.Name = nameVal;
                            }
                            if (product.Price > 0) products.Add(product);
                        }
                        catch (StaleElementReferenceException)
                        {
                            continue;
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }


                var cnt = products.Count();
            }
            catch (Exception)
            {

                throw;
            }

            return Task.FromResult(products);
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="MediaExpertScraper"/> class.
        /// </summary>
        public void Dispose()
        {
            _driver.Close();
            _driver.Quit();
            _driver.Dispose();
        }

    }
}
