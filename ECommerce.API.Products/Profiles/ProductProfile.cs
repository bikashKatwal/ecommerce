using ECommerce.API.Products.Db;
using ECommerce.API.Products.Models;

namespace ECommerce.API.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
