using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.MasterModel;
using System;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IExampleMasterService
    {
        Task<PagedList<ExampleMasterEntity>> GetAllAsync(MasterRequestDto requestDto);   

        Task<ExampleMasterEntity> GetByIdAsync(Guid id);
        Task AddAsync(ExampleMasterEntity entity);
        Task DeleteAsync(ExampleMasterEntity entity);
        Task UpdateAsync(ExampleMasterEntity entity);
        Task<bool> IsExistsAsync(Guid id);
    }
}
