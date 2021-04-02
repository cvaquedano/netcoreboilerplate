using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
