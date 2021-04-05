using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;

namespace NetCoreWebApiBoilerPlate.Models.MasterStatusModel
{
    public class MasterStatusBaseDto : ReadBaseDto
    {
        public Guid Id { get; set; }
    }
}
