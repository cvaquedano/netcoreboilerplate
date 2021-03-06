using NetCoreWebApiBoilerPlate.Models.BaseDtos;

namespace NetCoreWebApiBoilerPlate.Models.MasterDetailModel
{
    public class MasterDetailForCreateDto : WriteBaseDto
    {
        public string Value { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }

        public float Total { get; set; }
    }
}
