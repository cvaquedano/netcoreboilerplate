
using NetCoreWebApiBoilerPlate.Models.BaseDtos;

namespace NetCoreWebApiBoilerPlate.Models
{
    public class UsersRequestDto : PaginationRequestBaseDto
    {
        public override string OrderBy { get => base.OrderBy; set => base.OrderBy = value; }
    }
}
