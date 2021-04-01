using AutoMapper;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserBaseResponse>()
               .ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
              ;

            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterResponse>();
        }
    }
}
