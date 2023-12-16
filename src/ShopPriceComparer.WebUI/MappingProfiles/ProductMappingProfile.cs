using AutoMapper;
using ShopPriceComparer.Core.Models;
using ShopPriceComparer.WebUI.Models;

namespace ShopPriceComparer.WebUI.MappingProfiles
{
    /// <summary>
    /// This class is responsible for mapping between the Product and Category entities and their corresponding view models.
    /// It inherits from the AutoMapper Profile class.
    /// </summary>
    /// <returns>
    /// It does not return any value but it configures the mapping profiles for Category to CategoryViewModel and Product to ProductViewModel.
    /// </returns>
    public class ProductMappingProfile : Profile
    {
        /// <summary>
        /// This is a constructor for the ProductMappingProfile class. It creates a mapping profile for Category to CategoryViewModel and Product to ProductViewModel.
        /// </summary>
        /// <returns>
        /// Does not return a value.
        /// </returns>
        public ProductMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Product, ProductViewModel>();
        }

    }
}
