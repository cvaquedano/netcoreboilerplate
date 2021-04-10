using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Data.Repositories
{
    public class MasterDetailRepository: IMasterDetailRepository
    {
        private readonly Context _context;
        public MasterDetailRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(MasterDetailEntity entity)
        {
            await _context.MasterDetailEntity.AddAsync(entity);

        }

        public void Delete(MasterDetailEntity entity)
        {
            _context.MasterDetailEntity.Remove(entity);
        }

        public IQueryable<MasterDetailEntity> GetAll()
        {
            return _context.Set<MasterDetailEntity>().AsNoTracking();
        }

        public  Task<MasterDetailEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
            //return await _context.MasterDetailEntity.FindAsync(id);
        }
        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.MasterDetailEntity.AnyAsync(e => e.Id == id);
        }

        

        public void Update(MasterDetailEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


    }
}
