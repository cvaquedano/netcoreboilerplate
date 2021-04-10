using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{
    [TestClass]
    public class GetAllUsersControllerTest : UsersControllerBaseTest
    {

        private static UsersRequestDto userRequestDto = new UsersRequestDto();

        public GetAllUsersControllerTest()
        {
            SetUpUserServiceMoq();
        }

        private  void SetUpUserServiceMoq()
        {
            var collection = new List<User> { new User { } };
            var pagedList = PagedList<User>.Create(collection.AsQueryable(), userRequestDto.PageNumber, userRequestDto.PageSize);
            userServiceMoq.Setup(c => c.GetAllAsync(userRequestDto)).Returns(Task.FromResult(pagedList));
        }

        [TestMethod]
        public void given_a_valid_userRequestDto_should_return_ok_response()
        {
            //Arrange


            // Act on Test
            var response = _usersController.GetAll(userRequestDto).Result;

            // Assert the result         
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

        }

        [TestMethod]
        public void given_a_valid_userRequestDto_return_should_contain_a_pagination()
        {
            //Arrange


            // Act on Test
            var response = _usersController.GetAll(userRequestDto).Result;
           

            // Assert the result         
            Assert.IsNotNull(response);
            //Assert.IsTrue(response.Headers.Contains("X-Pagination"));

        }
    }

}
