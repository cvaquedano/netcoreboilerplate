using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Data.Repositories
{
    public class ExampleMasterRepository : IExampleMasterRepository
    {
        private readonly Context _context;
        public ExampleMasterRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(ExampleMasterEntity entity)
        {
            await _context.ExampleMasterEntity.AddAsync(entity);
          
        }

        public void Delete(ExampleMasterEntity entity)
        {
            _context.ExampleMasterEntity.Remove(entity);          
        }

        public  IQueryable<ExampleMasterEntity> GetAll()
        {
            return _context.Set<ExampleMasterEntity>().AsNoTracking();
        }

        public async Task<ExampleMasterEntity> GetByIdAsync(Guid id)
        {
            return await _context.ExampleMasterEntity.FindAsync(id);
        }
        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.ExampleMasterEntity.AnyAsync(e => e.Id == id);
        }

      

        public void Update(ExampleMasterEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

   
    }
}
