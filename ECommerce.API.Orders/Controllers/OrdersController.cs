using ECommerce.API.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider _orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await _orderProvider.GetOrderByCustomerId(customerId);
            if (result.IsSuccess) {
                return Ok(result.Orders);
            }
            return NotFound();
        }
    }
}
