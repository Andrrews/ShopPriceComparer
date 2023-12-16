namespace ShopPriceComparer.Core.Models{
    /// <summary>
    /// Represents a product with properties for the name, price, and category.
    /// </summary>
    public class Product    {        public string Name { get; set; }        public decimal Price { get; set; }        public Category Category { get; set; }    }}