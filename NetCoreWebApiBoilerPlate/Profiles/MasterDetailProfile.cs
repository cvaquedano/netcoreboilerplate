using AutoMapper;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models.MasterDetailModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class MasterDetailProfile : Profile
    {
        public MasterDetailProfile()
        {
            CreateMap<MasterDetailEntity, MasterDetailResponseDto>();


            CreateMap<MasterDetailForCreateDto, MasterDetailEntity>();
            CreateMap<MasterDetailForUpdateDto, MasterDetailEntity>();
        }
    }
}
