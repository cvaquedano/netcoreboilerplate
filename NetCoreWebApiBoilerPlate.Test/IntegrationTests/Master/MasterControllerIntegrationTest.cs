using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWebApiBoilerPlate.Models.MasterModel;
using NetCoreWebApiBoilerPlate.Models.MasterStatusModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.IntegrationTests.Master
{
    [TestClass]
    public class MasterControllerIntegrationTest : MasterIntegrationTestBase
    {
        [TestMethod]
        public async Task ShouldReturnSuccessResponseAsync()
        {
            await AuthenticateAsync();

            // Act

            var response = await TestClient.GetAsync("/api/master");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();
        }
        [TestMethod]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // Arrange    
            await AuthenticateAsync();
            var createdStatus = await CreateMasterStatusPostAsync(new MasterStatusForCreateDto
            {
                Value = "Integrationpost",
                Description = "Integration Test",
               
            });

            var createdPost = await CreateMasterPostAsync(new MasterForCreateDto
            {
                FirstName = "Integration post",
                LastName = "Integration Test",
                DOB = new DateTime(1988, 4, 14),
                MasterStatusEntityId = createdStatus.Id
            }); ;

            // Act           
            var response = await TestClient.GetAsync("api/master/{id}".Replace("{id}", createdPost.Id.ToString()));



            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var result = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(result.Contains(createdPost.Id.ToString()));
            Assert.IsTrue(result.Contains("Integration post"));


        }

    }
}
