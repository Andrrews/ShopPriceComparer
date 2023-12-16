using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for a scraper that scrapes product data from a website for a specific shop.
    /// </summary>
    public interface IScraper
    {
        /// <summary>
        /// Gets the shop for which this scraper is designed.
        /// </summary>
        Shop Shop { get; }

        /// <summary>
        /// Scrapes product data asynchronously from the specified URI.
        /// </summary>
        /// <param name="uri">The URI of the website to scrape.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the TResult parameter contains a list of scraped products.</returns>
        Task<List<Product>> ScrapAsync(Uri uri);
    }
}
