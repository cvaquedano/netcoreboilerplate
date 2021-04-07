using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApiBoilerPlate.Test.Helpers;
using NetCoreWebApiBoilerPlate.Models;

namespace NetCoreWebApiBoilerPlate.Test.IntegrationTests
{
    public class IntegrationTestBase
    {
        protected readonly HttpClient TestClient;
        protected IntegrationTestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(Context));
                        services.AddDbContext<Context>(options => { options.UseInMemoryDatabase("NetCoreBoilerPlateTestDb"); });
                    });
                });

            TestClient = appFactory.CreateClient();
            TestClient.DefaultRequestHeaders.Accept.Clear();
            TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }


        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            await TestClient.PostAsJsonAsync("api/users/register", new RegisterRequestDto
            {
                Email = "test@integration.com",
                Password = "SomePass1234!",
                Username = "test"
            });


            var authenticateResponse = await TestClient.PostAsJsonAsync("api/users/authenticate", new AuthenticateRequestDto
            {
                Email = "test@integration.com",
                Password = "SomePass1234!",
                Username = "test"
            });

            var authorizationResponse = await authenticateResponse.Content.ReadAsJsonAsync<AuthenticateResponseDto>();

            return authorizationResponse.Token;
        }

    }
}
