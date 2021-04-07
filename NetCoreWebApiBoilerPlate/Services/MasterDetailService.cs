using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using NetCoreWebApiBoilerPlate.Models.MasterDetailModel;
using NetCoreWebApiBoilerPlate.Repositories;
using NetCoreWebApiBoilerPlate.UnitsOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public class MasterDetailService : IMasterDetailService
    {
        public IUnitOfWork _unitOfWork { get; }
        public MasterDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<MasterDetailEntity>> GetAllForMasterAsync(Guid masterId, MasterDetailRequestDto requestDto)
        {
            if (requestDto is null)
            {
                throw new ArgumentNullException(nameof(requestDto));
            }
            var collection = await Task.FromResult(_unitOfWork.MasterDetailRepository.GetAll());

            collection = collection.Where(t => t.ExampleMasterEntityId == masterId);

            if (!string.IsNullOrWhiteSpace(requestDto.SearchQuery))
            {
                requestDto.SearchQuery = requestDto.SearchQuery.Trim();
                collection = collection.Where(a => a.Value.Contains(requestDto.SearchQuery));
            }
            return PagedList<MasterDetailEntity>.Create(collection, requestDto.PageNumber, requestDto.PageSize);
        }
        public Task<PagedList<MasterDetailEntity>> GetAllAsync(PaginationRequestBaseDto requestDto)
        {
            throw new NotImplementedException();
            //if (requestDto is null)
            //{
            //    throw new ArgumentNullException(nameof(requestDto));
            //}

            //var collection = await Task.FromResult(_repository.GetAll());

            //if (!string.IsNullOrWhiteSpace(requestDto.SearchQuery))
            //{
            //    requestDto.SearchQuery = requestDto.SearchQuery.Trim();
            //    collection = collection.Where(a => a.Value.Contains(requestDto.SearchQuery)
            //    );
            //}
            //return PagedList<MasterDetailEntity>.Create(collection, requestDto.PageNumber, requestDto.PageSize);
        }

        public  Task<MasterDetailEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
            //return await _repository.GetByIdAsync(id);
        }

        public async Task<MasterDetailEntity> GetByIdForMasterAsync(Guid masterId, Guid id)
        {
            return await Task.FromResult(_unitOfWork.MasterDetailRepository.GetAll().FirstOrDefault(t=>t.ExampleMasterEntityId==masterId && t.Id==id));
        }

        public async Task AddAsync(MasterDetailEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await _unitOfWork.MasterDetailRepository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(MasterDetailEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _unitOfWork.MasterDetailRepository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(MasterDetailEntity entity)
        {

            _unitOfWork.MasterDetailRepository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _unitOfWork.MasterDetailRepository.IsExistsAsync(id);
        }

      
    }

}
