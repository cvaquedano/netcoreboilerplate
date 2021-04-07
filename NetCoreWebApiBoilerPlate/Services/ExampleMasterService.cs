using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using NetCoreWebApiBoilerPlate.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class ExampleMasterService : IExampleMasterService
    {
       
        public IExampleMasterRepository _repository { get; }
        public ExampleMasterService(IExampleMasterRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<ExampleMasterEntity>> GetAllAsync(PaginationRequestBaseDto requestDto)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            var collection =await Task.FromResult( _repository.GetAll());

            if (!string.IsNullOrWhiteSpace(requestDto.SearchQuery))
            {
                requestDto.SearchQuery = requestDto.SearchQuery.Trim();
                collection = collection.Where(a => a.FirstName.Contains(requestDto.SearchQuery)
                || a.LastName.Contains(requestDto.SearchQuery));
            }
            return PagedList<ExampleMasterEntity>.Create(collection, requestDto.PageNumber, requestDto.PageSize);
        }

        public async Task<ExampleMasterEntity> GetByIdAsync(Guid id)
        {
            return  await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ExampleMasterEntity entity)
        {
            entity.Id = Guid.NewGuid();
          
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(ExampleMasterEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
             _repository.Delete(entity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(ExampleMasterEntity entity)
        {
             _repository.Update(entity);
            await _repository.SaveAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _repository.IsExistsAsync(id);
        }
    }
}
