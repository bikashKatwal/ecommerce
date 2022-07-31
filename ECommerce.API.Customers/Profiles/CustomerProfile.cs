using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Dtos;

namespace ECommerce.API.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
