using AutoMapper;
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
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Firstname + src.Lastname));

            CreateMap<User, UserDetailDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.Firstname + " "  + src.Lastname));


        }
    }
}
