using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.MasterDetailModel;
using System;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IMasterDetailService : IService<MasterDetailEntity>
    {
        Task<PagedList<MasterDetailEntity>> GetAllForMasterAsync(Guid masterId, MasterDetailRequestDto requestDto);
        Task<MasterDetailEntity> GetByIdForMasterAsync(Guid masterId, Guid id);
    }
}
