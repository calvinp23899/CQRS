using AutoMapper;
using Ecommerce.Core.DTOs.Orders;
using Ecommerce.Core.DTOs.Products;
using Ecommerce.Core.DTOs.User;
using Ecommerce.Core.Models;

namespace Ecommerce.API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Firstname + src.Lastname));

            CreateMap<User, UserDetailDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Firstname + " "  + src.Lastname));

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => src.UpdatedDate))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName));

            CreateMap<OrderDetail, OrderDetailDTO>()
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
               .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
               .ForMember(dest => dest.Total, opt => opt.MapFrom(src => (src.Quantity * src.UnitPrice).Value));

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalOrder, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.orderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Firstname + " " + src.User.Lastname));

            CreateMap<User, UserOrderHistoryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.HistoryOrders, opt => opt.MapFrom(src => src.Orders))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Firstname + src.Lastname));

            CreateMap<Order, HistoryDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TotalOrder, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.orderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            //CreateMap<OrderDetail, OrderDetailDTO>()
            //    .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            //    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            //    .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            //    .ForMember(dest => dest.Total, opt => opt.MapFrom(src => (src.Quantity * src.UnitPrice).Value));

        }
    }
}
