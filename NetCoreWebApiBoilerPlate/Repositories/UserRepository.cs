using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Linq;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        public UserRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.User.Add(entity);
        }

        public User Authenticate(string username, string email)
        {
           return  _context.User.FirstOrDefault(x => x.Username == username || x.Email == email);
        }

        public void Delete(User entity)
        {
            _context.User.Remove(entity);
        }

        public IQueryable<User> GetAll()
        {
            return _context.User.AsQueryable(); 
        }

        public User GetById(Guid id)
        {
            return _context.User.FirstOrDefault(x => x.Id == id);
        }

        public bool IsExists(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.User.Any(a => a.Id == id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
          
        }

        public void Update(User entity)
        {
           
        }
    }
}
