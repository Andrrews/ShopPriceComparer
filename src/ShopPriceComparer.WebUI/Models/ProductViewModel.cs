namespace ShopPriceComparer.WebUI.Models{
    /// <summary>
    /// Represents a view model for a product, containing properties for the shop name, product name, price, and category.
    /// </summary>
    public class ProductViewModel    {        public string ShopName { get; set; }        public string Name { get; set; }        public decimal Price { get; set; }        public CategoryViewModel Category { get; set; }    }

    /// <summary>
    /// Represents a matched product from two different shops. This class holds two properties, Shop1Product and Shop2Product, which are instances of the ProductViewModel class.
    /// </summary>
    public class MatchedProduct    {        public ProductViewModel Shop1Product { get; set; }        public ProductViewModel Shop2Product { get; set; }    }}