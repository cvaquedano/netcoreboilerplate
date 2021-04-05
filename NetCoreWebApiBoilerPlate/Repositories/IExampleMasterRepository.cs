using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public interface IExampleMasterRepository
    {
        IQueryable<ExampleMasterEntity> GetAll();

        Task<ExampleMasterEntity> GetByIdAsync(Guid id);
        Task AddAsync(ExampleMasterEntity entity);
        Task<bool> IsExistsAsync(Guid id);
        Task<bool> SaveAsync();

        void Delete(ExampleMasterEntity entity);
        void Update(ExampleMasterEntity entity);

       
    }
}
