using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.Db
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
