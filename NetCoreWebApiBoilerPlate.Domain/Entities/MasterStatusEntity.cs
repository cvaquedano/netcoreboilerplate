using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public class MasterStatusEntity : BaseEntity
    {
        public string Value { get; set; }
        public string Description { get; set; }
     
    }
}
