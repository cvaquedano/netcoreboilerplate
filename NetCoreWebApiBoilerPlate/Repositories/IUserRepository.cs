using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> Authenticate(string username, string email);
    }
}
