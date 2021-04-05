
using NetCoreWebApiBoilerPlate.Models.BaseDtos;

namespace NetCoreWebApiBoilerPlate.Models
{
    public class UsersRequestDto : PaginationRequestBaseDto
    {
        public UsersRequestDto()
        {
            OrderBy = "Name";
        }
     
    }
}
