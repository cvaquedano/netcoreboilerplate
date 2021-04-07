using NetCoreWebApiBoilerPlate.Models.BaseDtos;

namespace NetCoreWebApiBoilerPlate.Models.MasterStatusModel
{
    public class MasterStatusForUpdateDto : WriteBaseDto
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
