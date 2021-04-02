using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using System;
using System.Collections.Generic;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IUserService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto model);
        PagedList<User> GetAll(UsersRequestDto usersRequestDto);
        User GetById(Guid id);

        void Register(User userEntity);
        bool IsEntityExist(Guid userId);
        void Update(User userEntity);
    }
}
