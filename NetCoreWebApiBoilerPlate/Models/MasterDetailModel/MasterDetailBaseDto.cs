using NetCoreWebApiBoilerPlate.Models.BaseDtos;
using System;

namespace NetCoreWebApiBoilerPlate.Models.MasterDetailModel
{
    public class MasterDetailBaseDto : ReadBaseDto
    {
        public Guid Id { get; set; }
    }
}
