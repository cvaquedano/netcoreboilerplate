using AutoMapper;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Models.MasterStatusModel;

namespace NetCoreWebApiBoilerPlate.Profiles
{
    public class MasterStatusProfile : Profile
    {
        public MasterStatusProfile()
        {
            CreateMap<MasterStatusEntity, MasterStatusResponseDto>();


            CreateMap<MasterStatusForCreateDto, MasterStatusEntity>();
            CreateMap<MasterStatusForUpdateDto, MasterStatusEntity>();
        }
    }
}
