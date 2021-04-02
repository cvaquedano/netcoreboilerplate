using AutoMapper;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class BaseProfile : Profile
    {
        public BaseProfile()
        {
            CreateMap<BaseEntity, ReadBaseDto>();

            CreateMap<BaseEntity, WriteBaseDto>();
           


            CreateMap<ReadBaseDto, BaseEntity>();
            CreateMap<WriteBaseDto, BaseEntity>();
        }
      
    }
}
