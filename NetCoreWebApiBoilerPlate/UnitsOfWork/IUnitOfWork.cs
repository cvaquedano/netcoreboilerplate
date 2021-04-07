using NetCoreWebApiBoilerPlate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ExampleMasterRepository ExampleMasterRepository { get; }
        MasterDetailRepository MasterDetailRepository { get; }
        MasterStatusRepository MasterStatusRepository { get; }
        UserRepository UserRepository { get; }
        Task<bool> SaveAsync();
    }
}
