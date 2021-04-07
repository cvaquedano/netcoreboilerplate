using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Models.MasterModel;
using NetCoreWebApiBoilerPlate.Models.MasterStatusModel;
using NetCoreWebApiBoilerPlate.Test.Helpers;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.IntegrationTests.Master
{
    public class MasterIntegrationTestBase : IntegrationTestBase
    {
        protected async Task<MasterResponseDto> CreateMasterPostAsync( MasterForCreateDto request)
        {
            var response = await TestClient.PostAsJsonAsync<MasterForCreateDto>("/api/master/post", request);
            return await response.Content.ReadAsJsonAsync<MasterResponseDto>();
        }

        protected async Task<MasterStatusResponseDto> CreateMasterStatusPostAsync(MasterStatusForCreateDto request)
        {
            var response = await TestClient.PostAsJsonAsync<MasterStatusForCreateDto>("/api/masterstatus/post", request);
            return await response.Content.ReadAsJsonAsync<MasterStatusResponseDto>();
        }

    }
}
