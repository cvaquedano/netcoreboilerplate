using NetCoreWebApiBoilerPlate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        public UnitOfWork(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            ExampleMasterRepository = new ExampleMasterRepository(_context);
            MasterDetailRepository = new MasterDetailRepository(_context);
            MasterStatusRepository = new MasterStatusRepository(_context);
            UserRepository = new UserRepository(_context);
        }
        public ExampleMasterRepository ExampleMasterRepository { get; private set; }

        public MasterDetailRepository MasterDetailRepository { get; private set; }

        public MasterStatusRepository MasterStatusRepository { get; private set; }

        public UserRepository UserRepository { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return (result >= 0);
        }
    }
}
