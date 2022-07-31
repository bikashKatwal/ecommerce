using ECommerce.API.Search.Interfaces;

namespace ECommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;

        public SearchService(IOrderService orderService,
            IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var orderResult = await _orderService.GetOrdersAsync(customerId);
            var productResult = await _productService.GetProductsAsync();
            var customerResult = await _customerService.GetCustomerAsync(customerId);
            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {

                        item.ProductName = productResult.IsSuccess
                            ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                            : "Product Information is not available";
                    }
                }


                var result = new
                {
                    customerResult.Customer,
                    orderResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
