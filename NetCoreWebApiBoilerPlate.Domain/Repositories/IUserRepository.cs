using NetCoreWebApiBoilerPlate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        Task<User> Authenticate(string username, string email);
    }
}
