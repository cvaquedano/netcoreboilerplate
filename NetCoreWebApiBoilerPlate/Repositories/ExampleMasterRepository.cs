using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public class ExampleMasterRepository : IExampleMasterRepository
    {
        private readonly Context _context;
        public ExampleMasterRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(ExampleMasterEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ExampleMasterEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExampleMasterEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public ExampleMasterEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ExampleMasterEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
