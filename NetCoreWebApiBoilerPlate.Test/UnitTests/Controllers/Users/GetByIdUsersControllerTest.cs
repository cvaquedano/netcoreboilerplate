using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{
    [TestClass]
    public class GetByIdUsersControllerTest : UsersControllerBaseTest
    {
        public GetByIdUsersControllerTest()
        {
            SetUpUserServiceMoq();

        }

        private  void SetUpUserServiceMoq()
        {
            userServiceMoq.Setup(c => c.GetByIdAsync(_invalidadUserId)).Returns(Task.FromResult((User)null));

            var userObject = new User { Id = _validadUserId, FirstName = "ValidFirstName", LastName = "ValidLastName" };
            userServiceMoq.Setup(c => c.GetByIdAsync(_validadUserId)).Returns(Task.FromResult(userObject));
        }


        [TestMethod]
        public void given_a_invalid_userId_should_return_a_not_found_response()
        {
            //Arrange

            // Act on Test
            var response = _usersController.GetById(_invalidadUserId).Result;

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));

        }
      

        [TestMethod]
        public void given_a_valid_userId_should_return_ok_response()
        {
            //Arrange
          
            // Act on Test
            var response = _usersController.GetById(_validadUserId).Result;

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

        }

       
    }

}
