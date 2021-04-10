using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using NetCoreWebApiBoilerPlate.Data.UnitsOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class MasterStatusService : IMasterStatusService
    {
        public IUnitOfWork _unitOfWork { get; }
        public MasterStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<MasterStatusEntity>> GetAllAsync(PaginationRequestBaseDto requestDto)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            var collection = await Task.FromResult(_unitOfWork.MasterStatusRepository.GetAll());

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
            return await _unitOfWork.MasterStatusRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(MasterStatusEntity entity)
        {
            entity.Id = Guid.NewGuid();

            await _unitOfWork.MasterStatusRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(MasterStatusEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _unitOfWork.MasterStatusRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(MasterStatusEntity entity)
        {
            _unitOfWork.MasterStatusRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _unitOfWork.MasterStatusRepository.IsExistsAsync(id);
        }
    }
}
