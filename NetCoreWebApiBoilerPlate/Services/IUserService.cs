using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models;
using System;
using System.Collections.Generic;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);

        void Register(User userEntity);
    }
}
