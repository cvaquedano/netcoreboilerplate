using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IService<T> where T: BaseEntity
    {
        Task<PagedList<T>> GetAllAsync(PaginationRequestBaseDto requestDto);

        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> IsExistsAsync(Guid id);
    }
}
