using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Data.Repositories
{
    public class MasterStatusRepository : IMasterStatusRepository
    {
        private readonly Context _context;
        public MasterStatusRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(MasterStatusEntity entity)
        {
            await _context.MasterStatusEntity.AddAsync(entity);

        }

        public void Delete(MasterStatusEntity entity)
        {
            _context.MasterStatusEntity.Remove(entity);
        }

        public IQueryable<MasterStatusEntity> GetAll()
        {
            return _context.Set<MasterStatusEntity>().AsNoTracking();
        }

        public async Task<MasterStatusEntity> GetByIdAsync(Guid id)
        {
            return await _context.MasterStatusEntity.FindAsync(id);
        }
        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.MasterStatusEntity.AnyAsync(e => e.Id == id);
        }

       

        public void Update(MasterStatusEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


    }
}
