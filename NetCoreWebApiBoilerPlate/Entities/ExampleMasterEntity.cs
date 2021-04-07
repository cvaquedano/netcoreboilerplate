using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public Guid MasterStatusEntityId { get; set; }

        public List<MasterDetailEntity> MasterDetailEntities { get; set; } =  new List<MasterDetailEntity>();
    }
}
