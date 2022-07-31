using AutoMapper;
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Dtos;
using ECommerce.API.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _dbContext;
        private readonly ILogger<CustomerProvider> _logger;
        private readonly IMapper _mapper;

        public CustomerProvider(CustomerDbContext dbContext,
            ILogger<CustomerProvider> logger,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                var customers = new List<Customer> {
                    new Customer { Id = 1, Name = "Bikash", Address="1 Glunda Street" },
                     new Customer { Id = 2, Name = "Deepa", Address="1 Glynda Street"},
                      new Customer { Id = 3, Name = "Ram", Address="110 Glynda Street"},
                       new Customer { Id = 4, Name = "Hari",Address="120 Glynda Street" },
                       new Customer { Id = 5, Name = "Sita", Address="130 Glynda Street" },
                };
                _dbContext.Customers.AddRange(customers);
                _dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<CustomerDto> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var products = await _dbContext.Customers.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(products);
                    return (IsSuccess: true, Customers: result, ErrorMessage: null);
                }
                return (IsSuccess: false, null, ErrorMessage: "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (IsSuccess: false, Customers: null, ErrorMessage: ex.Message);
            }
        }

        public async Task<(bool IsSuccess, CustomerDto Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var product = await _dbContext.Customers.FirstOrDefaultAsync<Customer>(p => p.Id == id);
                if (product != null)
                {
                    var result = _mapper.Map<Customer, CustomerDto>(product);
                    return (true, Customer: result, null);
                }
                return (false, Customer: null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (IsSuccess: false, null, ErrorMessage: ex.Message);
            }
        }
    }
}
