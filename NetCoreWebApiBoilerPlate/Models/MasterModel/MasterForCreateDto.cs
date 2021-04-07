using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;

namespace NetCoreWebApiBoilerPlate.Models.MasterModel
{
    public class MasterForCreateDto : WriteBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public bool Gender { get; set; }

        public Guid MasterStatusEntityId { get; set; }
    }
}
