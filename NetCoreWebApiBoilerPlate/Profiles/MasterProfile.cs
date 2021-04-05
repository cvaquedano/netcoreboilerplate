using AutoMapper;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models.MasterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class MasterProfile : Profile
    {
        public MasterProfile()
        {
            CreateMap<ExampleMasterEntity, MasterResponseDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));


            CreateMap<MasterForCreateDto, ExampleMasterEntity>();
            CreateMap<MasterForUpdateDto, ExampleMasterEntity>();
        }
    }
}
