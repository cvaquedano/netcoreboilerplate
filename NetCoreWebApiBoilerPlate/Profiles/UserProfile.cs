﻿using AutoMapper;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterResponseDto>().ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
              ;
            CreateMap<User, UserBaseDto>()
               .ForMember(
                   dest => dest.Name,
                   opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
              ;

            CreateMap<RegisterRequestDto, User>();
           
        }
    }
}