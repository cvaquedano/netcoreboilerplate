using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;

namespace NetCoreWebApiBoilerPlate.Models.MasterStatusModel
{
    public abstract class MasterStatusBaseDto : ReadBaseDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
