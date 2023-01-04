using AutoMapper;
using Super4.Application.DataContract.Request.Customer;
using Super4.Application.DataContract.Request.Order;
using Super4.Application.DataContract.Request.Product;
using Super4.Application.DataContract.Request.Stock;
using Super4.Application.DataContract.Response.Customer;
using Super4.Application.DataContract.Response.Order;
using Super4.Application.DataContract.Response.Product;
using Super4.Application.DataContract.Response.Stock;
using Super4.Domain.Model;

namespace Super4.Application.Mapper
{
    public class Core : Profile
    {
        public Core()
        {
            CustomerMap();
            ProductMap();
            StockMap();
            OrderMap();
        }

        private void CustomerMap()
        {
            CreateMap<CreateCustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
        private void ProductMap()
        {
            CreateMap<CreateProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
            CreateMap<UpdateProductRequest, Product>();
        }
        private void StockMap()
        {
            CreateMap<CreateStockRequest, Stock>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<Stock, StockResponse>()
                .ForPath(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<UpdateStockRequest, Stock>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));

        }
        private void OrderMap()
        {
            CreateMap<CreateOrderRequest, Order>()
                .ForPath(dest => dest.Customer.Id, opt => opt.MapFrom(src => src.CustomerId));
            CreateMap<CreateOrderItemRequest, OrderItem>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId));

            //////
            CreateMap<FillOrder, Order>()
                .ForPath(dest => dest.Customer.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.Customer.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Customer.Document, opt => opt.MapFrom(src => src.Document))
                .ForPath(dest => dest.Customer.CEP, opt => opt.MapFrom(src => src.CEP))
                .ForPath(dest => dest.Customer.AddressNumber, opt => opt.MapFrom(src => src.AddressNumber));
            ///////

            CreateMap<Order, OrderResponse>()
                .ForPath(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForPath(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id));


            CreateMap<OrderItem, OrderItemResponse>();
        }
    }
}
