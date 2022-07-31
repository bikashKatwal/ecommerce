using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Models;

namespace ECommerce.API.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderDto> Orders, string ErrorMessage)> GetOrderByCustomerId(int CustomerId);
    }
}
