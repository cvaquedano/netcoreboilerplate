using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IUserService : IService<User>
    {
        Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model);

    
     
    }
}
