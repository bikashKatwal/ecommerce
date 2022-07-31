
using ECommerce.API.Products.Models;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
