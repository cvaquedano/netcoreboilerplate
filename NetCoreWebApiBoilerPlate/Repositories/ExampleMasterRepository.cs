using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
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

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return (result >= 0);
        }

        public void Update(ExampleMasterEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

   
    }
}
