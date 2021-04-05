using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Models;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{
    [TestClass]
    public class AuthenticateUsersControllerTest : UsersControllerBaseTest
    {
        public AuthenticateUsersControllerTest()
        {
            SetUpUserServiceMoq();
        }

        private void SetUpUserServiceMoq()
        {
            var userObject = new User();
            var invalidModel = new AuthenticateRequestDto { Username = "Invalid" };
            var validModel = new AuthenticateRequestDto { Username = "Valid" };
            var returnObject = new AuthenticateResponseDto(userObject, "fakeToken");

            userServiceMoq.Setup(c => c.AuthenticateAsync(It.IsAny<AuthenticateRequestDto>())).Returns(Task.FromResult(returnObject));
            
        }

        [TestMethod]
        public void when_authentica_a_valid_user_should_return_ok()
        {
            // Arrange           
            var validModel = new AuthenticateRequestDto { Username = "Valid" };

            // Act
            var response = _usersController.Authenticate(validModel).Result;

            // Assert
            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }
    }

}
