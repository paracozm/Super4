using AutoMapper;
using Super4.Application.DataContract.Request.Customer;
using Super4.Application.DataContract.Response.Customer;
using Super4.Domain.Model;

namespace Super4.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            CustomerMap();
        }

        private void CustomerMap()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}
