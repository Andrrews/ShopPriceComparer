namespace ShopPriceComparer.Core.Models{

    /// <summary>
    /// Represents a product in a shop, including properties for the shop name, product name, price, and category.
    /// </summary>
    public class Product    {        public string ShopName { get; set; }        public string Name { get; set; }        public decimal Price { get; set; }        public Category Category { get; set; }    }}