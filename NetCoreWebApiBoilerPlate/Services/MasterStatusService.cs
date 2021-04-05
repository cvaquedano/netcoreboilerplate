using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using NetCoreWebApiBoilerPlate.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class MasterStatusService : IMasterStatusService
    {
        public IMasterStatusRepository _repository { get; }
        public MasterStatusService(IMasterStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedList<MasterStatusEntity>> GetAllAsync(PaginationRequestBaseDto requestDto)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            var collection = await Task.FromResult(_repository.GetAll());

            if (!string.IsNullOrWhiteSpace(requestDto.SearchQuery))
            {
                requestDto.SearchQuery = requestDto.SearchQuery.Trim();
                collection = collection.Where(a => a.Value.Contains(requestDto.SearchQuery)
                || a.Description.Contains(requestDto.SearchQuery));
            }
            return PagedList<MasterStatusEntity>.Create(collection, requestDto.PageNumber, requestDto.PageSize);
        }

        public async Task<MasterStatusEntity> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(MasterStatusEntity entity)
        {
            entity.Id = Guid.NewGuid();

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(MasterStatusEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _repository.Delete(entity);
            await _repository.SaveAsync();
        }

        public async Task UpdateAsync(MasterStatusEntity entity)
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
