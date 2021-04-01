using NetCoreWebApiBoilerPlate.Entities;
using System;
using System.Collections.Generic;

namespace NetCoreWebApiBoilerPlate.Repositories
{
    public interface IExampleMasterRepository
    {
        IEnumerable<ExampleMasterEntity> GetAll();
        ExampleMasterEntity GetById(Guid id);
        void Add(ExampleMasterEntity entity);
        void Delete(ExampleMasterEntity entity);
        void Update(ExampleMasterEntity entity);
        bool IsExists(Guid id);
        bool Save();
    }
}
