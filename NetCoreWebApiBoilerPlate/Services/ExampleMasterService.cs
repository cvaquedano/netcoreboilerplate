using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using NetCoreWebApiBoilerPlate.Data.UnitsOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class ExampleMasterService : IExampleMasterService
    {
       
        public IUnitOfWork _unitOfWork { get; }
        public ExampleMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<ExampleMasterEntity>> GetAllAsync(PaginationRequestBaseDto requestDto)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }

            var collection =await Task.FromResult( _unitOfWork.ExampleMasterRepository.GetAll());

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
            return  await _unitOfWork.ExampleMasterRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(ExampleMasterEntity entity)
        {
            entity.Id = Guid.NewGuid();
          
            await _unitOfWork.ExampleMasterRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(ExampleMasterEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
             _unitOfWork.ExampleMasterRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ExampleMasterEntity entity)
        {
             _unitOfWork.ExampleMasterRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _unitOfWork.ExampleMasterRepository.IsExistsAsync(id);
        }
    }
}
