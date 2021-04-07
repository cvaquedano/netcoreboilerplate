namespace NetCoreWebApiBoilerPlate.Models.MasterDetailModel
{
    public class MasterDetailResponseDto : MasterDetailBaseDto
    {
        public string Value { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }

        public float Total { get; set; }
    }
}
