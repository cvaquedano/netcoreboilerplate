using AutoMapper;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Models.UserModel;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterResponseDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<User, UserResponseDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
           

            CreateMap<RegisterRequestDto, User>();
            CreateMap<UserForUpdateDto, User>();


        }
    }
}
