using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
       
        public async Task AddAsync(User entity)
        {
            await _context.User.AddAsync(entity);

        }

        public async Task<User> Authenticate(string username, string email)
        {
           return  await _context.User.FirstOrDefaultAsync(x => x.Username == username || x.Email == email);
        }

        public void Delete(User entity)
        {
            _context.User.Remove(entity);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Set<User>().AsNoTracking();
        }

      
        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.User.FindAsync(id);
        }


        public async Task<bool> IsExistsAsync(Guid id)
        {
            return await _context.User.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return (result >= 0);
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
