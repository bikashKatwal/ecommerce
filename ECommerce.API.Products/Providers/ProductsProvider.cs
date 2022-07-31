using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductsDbContext dbContext,
            ILogger<ProductsProvider> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                var products = new List<Product> {
                    new Product { Id = 1, Name = "Laptop", Price = 10, Inventory = 100 },
                     new Product { Id = 2, Name = "Keyboard", Price = 20, Inventory = 200 },
                      new Product { Id = 3, Name = "Mouse", Price = 30, Inventory = 300 },
                       new Product { Id = 4, Name = "Monitor", Price = 40, Inventory = 400 },
                       new Product { Id = 5, Name = "CPU", Price = 50, Inventory = 500 },
                };
                _dbContext.Products.AddRange(products);
                _dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<ProductDto> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
                    return (IsSuccess: true, Products: result, ErrorMessage: null);
                }
                return (IsSuccess: false, null, ErrorMessage: "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (IsSuccess: false, Products: null, ErrorMessage: ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ProductDto Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync<Product>(p => p.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<Product, ProductDto>(product);
                    return (true, Products: result, null);
                }
                return (false, Products: null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (IsSuccess: false, null, ErrorMessage: ex.Message);
            }
        }
    }
}
