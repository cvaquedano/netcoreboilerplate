using NetCoreWebApiBoilerPlate.Models.BaseDtos;

namespace NetCoreWebApiBoilerPlate.Models.MasterStatusModel
{
    public class MasterStatusForCreateDto : WriteBaseDto
    {
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
