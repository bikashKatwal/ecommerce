using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Db;

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) 
        : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
}

