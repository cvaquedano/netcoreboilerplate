using NetCoreWebApiBoilerPlate.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Data.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        MasterRepository ExampleMasterRepository { get; }
        MasterDetailRepository MasterDetailRepository { get; }
        MasterStatusRepository MasterStatusRepository { get; }
        UserRepository UserRepository { get; }
        Task<bool> SaveAsync();
    }
}
