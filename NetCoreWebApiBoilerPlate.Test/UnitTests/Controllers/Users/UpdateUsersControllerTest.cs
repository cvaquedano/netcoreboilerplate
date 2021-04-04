using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWebApiBoilerPlate.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{
    [TestClass]
    public class UpdateUsersControllerTest : UsersControllerBaseTest
    {
        public UpdateUsersControllerTest()
        {
            SetUpRestApiRepositoryMoq();
        }
        private void SetUpRestApiRepositoryMoq()
        {
            userServiceMoq.Setup(c => c.IsEntityExist(_invalidadUserId)).Returns(false);
            userServiceMoq.Setup(c => c.IsEntityExist(_validadUserId)).Returns(true);
        }


        [TestMethod]
        public void when_put_a_user_with_valid_user_id_should_return_no_content_result()
        {
            // Arrange 
            var userId = _validadUserId;
            var userDto = new UserForUpdateDto { FirstName = "TestName", LastName = "TestLastName" };

            // Act
            var response = _usersController.UpdateUser(userId, userDto);


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NoContentResult));
        }
        [TestMethod]
        public void when_put_a_user_with_invalid_user_id_should_return_no_found_result()
        {
            // Arrange 
            var userId = _invalidadUserId;
            var userDto = new UserForUpdateDto { FirstName = "TestName", LastName = "TestLastName" };

            // Act
            var response = _usersController.UpdateUser(userId, userDto);


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }
}
