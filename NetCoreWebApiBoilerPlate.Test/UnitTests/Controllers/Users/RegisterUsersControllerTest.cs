using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreWebApiBoilerPlate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{
    [TestClass]
    public class RegisterUsersControllerTest : UsersControllerBaseTest
    {
        public RegisterUsersControllerTest()
        {

        }

        [TestMethod]
        public void when_post_new_user_should_return_Route_name_GetUser()
        {
            // Arrange           
            var userDto = new RegisterRequestDto { FirstName = "TestName", LastName = "TestLastName" };

            // Act
            var response = _usersController.Register(userDto);
            var createdResult = response.Result as CreatedAtRouteResult;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetUser", createdResult.RouteName);
            Assert.AreEqual(createdResult.StatusCode, 201);
        }
    }

}
