using NetCoreWebApiBoilerPlate.Models.BaseDtos;

namespace NetCoreWebApiBoilerPlate.Models.MasterDetailModel
{
    public class MasterDetailRequestDto : PaginationRequestBaseDto
    {
        public MasterDetailRequestDto()
        {
            OrderBy = "Value";
        }
    }
}
