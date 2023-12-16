namespace ShopPriceComparer.WebUI.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}
