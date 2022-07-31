using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Models;

namespace ECommerce.API.Orders.Profiles
{
    public class OrderProfile:AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();

        }
    }
}
