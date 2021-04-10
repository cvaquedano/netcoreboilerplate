using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public class MasterDetailEntity : BaseEntity
    {
        public string Value { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }

        public float Total { get; set; }

        [ForeignKey("ExampleMasterEntityId")]
        public ExampleMasterEntity ExampleMasterEntity { get; set; }

        public Guid ExampleMasterEntityId { get; set; }
    }
}
