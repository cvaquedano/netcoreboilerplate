using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{
    [TestClass]
    public class DeleteUsersControllerTest : UsersControllerBaseTest
    {
        public DeleteUsersControllerTest()
        {
            SetUpUserServiceMoq();
        }
        private void SetUpUserServiceMoq()
        {
            userServiceMoq.Setup(c => c.GetByIdAsync(_invalidadUserId)).Returns(Task.FromResult((User)null));

            var userObject = new User { Id = _validadUserId, FirstName = "ValidFirstName", LastName = "ValidLastName" };
            userServiceMoq.Setup(c => c.GetByIdAsync(_validadUserId)).Returns(Task.FromResult(userObject));
        }

        [TestMethod]
        public void when_delete_a_user_with_valid_user_id_should_return_no_content_result()
        {
            // Arrange 
            var userId = _validadUserId;

            // Act
            var response = _usersController.Delete(userId).Result;


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NoContentResult));
        }
        [TestMethod]
        public void when_delete_a_user_with_invalid_user_id_should_return_no_found_result()
        {
            // Arrange 
            var userId = _invalidadUserId;

            // Act
            var response = _usersController.Delete(userId).Result;


            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }
    }

}
