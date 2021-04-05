using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApiBoilerPlate.Entities
{
    public class ExampleMasterEntity : BaseEntity
    {
      

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public bool Gender { get; set; }

        public MasterStatusEntity MasterStatusEntity { get; set; }

        public List<MasterDetailEntity> MasterDetailEntities { get; set; } =  new List<MasterDetailEntity>();
    }
}
