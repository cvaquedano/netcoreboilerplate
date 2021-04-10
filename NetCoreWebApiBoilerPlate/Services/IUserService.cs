using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Models;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Services
{
    public interface IUserService : IService<NetCoreWebApiBoilerPlate.Domain.Entities.User>
    {
        Task<AuthenticateResponseDto> AuthenticateAsync(AuthenticateRequestDto model);

    
     
    }
}
