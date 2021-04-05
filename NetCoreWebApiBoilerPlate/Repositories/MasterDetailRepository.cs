using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
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

        public async Task<MasterDetailEntity> GetByIdAsync(Guid id)
        {
            return await _context.MasterDetailEntity.FindAsync(id);
        }
        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.MasterDetailEntity.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return (result >= 0);
        }

        public void Update(MasterDetailEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


    }
}
