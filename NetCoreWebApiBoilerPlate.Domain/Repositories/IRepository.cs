using NetCoreWebApiBoilerPlate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Domain.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id);
    

        IQueryable<T> GetAll();
      

        Task AddAsync(T item);
     

        Task<bool> IsExistsAsync(Guid id);
   
        void Delete(T entity);
        void Update(T entity);
    }
}
