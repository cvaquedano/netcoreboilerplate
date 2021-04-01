using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Add(User entity);
        void Delete(User entity);
        void Update(User entity);
        bool IsExists(Guid id);
        User Authenticate(string username, string password);

        bool Save();
    }
}
