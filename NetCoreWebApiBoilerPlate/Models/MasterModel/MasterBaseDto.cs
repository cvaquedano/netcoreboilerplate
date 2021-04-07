using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;

namespace NetCoreWebApiBoilerPlate.Models.MasterModel
{
    public abstract class MasterBaseDto :   ReadBaseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DOB { get; set; }

        public bool Gender { get; set; }

        public Guid MasterStatusEntityId { get; set; }
    }
}
