using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Factories
{
    /// <summary>
    /// A factory class to create scraper instances based on the specified shop.
    /// </summary>
    public class ScraperFactory
    {
        private readonly IEnumerable<IScraper> _scrapers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScraperFactory"/> class with the provided collection of scrapers.
        /// </summary>
        /// <param name="scrapers">A collection of scrapers to be used for creating scraper instances.</param>
        public ScraperFactory(IEnumerable<IScraper> scrapers)
        {
            _scrapers = scrapers;
        }

        /// <summary>
        /// Creates a new instance of the scraper for the specified shop.
        /// </summary>
        /// <param name="shop">The shop for which the scraper should be created.</param>
        /// <returns>A new instance of the <see cref="IScraper"/> for the specified shop.</returns>
        /// <exception cref="ArgumentException">Thrown when no scraper is available for the specified shop.</exception>
        public IScraper CreateScraper(Shop shop)
        {
            var scraper = _scrapers.FirstOrDefault(s => s.Shop == shop);

            if (scraper == null)
            {
                throw new ArgumentException($"No scraper available for shop: {shop}");
            }

            return scraper;
        }
    }
}
