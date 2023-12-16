using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Services
{
    /// <summary>
    /// A scraper implementation for the Media Expert shop.
    /// </summary>
    public class MediaExpertScraper : IScraper, IDisposable
    {
        /// <summary>
        /// Gets the shop for which this scraper is designed.
        /// </summary>
        public Shop Shop => Shop.MediaExpert;
        private readonly IWebDriver _driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaExpertScraper"/> class with the provided WebDriver.
        /// </summary>
        /// <param name="driver">The WebDriver to be used for scraping.</param>
        public MediaExpertScraper(IWebDriver driver)
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
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(2));
                IReadOnlyList<IWebElement> offerBox = _driver.FindElements(By.ClassName("offer-box"));
                foreach (IWebElement e in offerBox)
                {
                    try
                    {
                        decimal price = 0;
                        Product product = new Product();

                        var priceColl = e.FindElements(By.ClassName("prices"));
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
                        var nameColl = e.FindElements(By.ClassName("name"));
                        if (nameColl.Count > 0)
                        {
                            var nameVal = nameColl.First().Text;
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

                var cnt = products.Count();
            }
            catch (Exception ex)
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
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
