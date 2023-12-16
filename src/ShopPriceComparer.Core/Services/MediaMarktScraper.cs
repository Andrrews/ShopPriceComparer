using ShopPriceComparer.Core.Interfaces;
using ShopPriceComparer.Core.Models;

namespace ShopPriceComparer.Core.Services
{
    /// <summary>
    /// This class represents a scraper for the MediaMarkt shop. It implements the IScraper interface.
    /// </summary>
    /// <returns>
    /// The ScrapAsync method returns a Task that represents a list of products from the MediaMarkt shop.
    /// </returns>
    public class MediaMarktScraper : IScraper
    {
        public Shop Shop => Shop.MediaMarkt;
        public Task<List<Product>> ScrapAsync(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
