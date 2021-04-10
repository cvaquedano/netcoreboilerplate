using AutoMapper;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Models.MasterModel;


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
