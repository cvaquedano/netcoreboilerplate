using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreWebApiBoilerPlate.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key, Column(Order = 0)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        //[ConcurrencyCheck]
        //[Timestamp]
        //public byte[] Version { get; set; }
    }
}
