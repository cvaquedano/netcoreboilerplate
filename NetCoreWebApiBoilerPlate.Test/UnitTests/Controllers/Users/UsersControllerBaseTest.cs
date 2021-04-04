using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using NetCoreWebApiBoilerPlate.Controllers;
using NetCoreWebApiBoilerPlate.Profiles;
using NetCoreWebApiBoilerPlate.Services;
using System;

namespace NetCoreWebApiBoilerPlate.Test.UnitTests.Controllers.Users
{

    public class UsersControllerBaseTest
    {
        protected readonly IUserService _userService;
        private IMapper _mapper;
        protected Mock<IUserService> userServiceMoq = new Mock<IUserService>();
        protected readonly UsersController _usersController;

        protected static readonly Guid _invalidadUserId = new Guid("8FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");
        protected static readonly Guid _validadUserId = new Guid("9FA566F0-8C0A-4DAA-A0B6-03CAD6D410BE");

        public UsersControllerBaseTest()
        {
            Mock<IUrlHelper> mockUrlHelper = SetUpMockUrlHelper();
            ControllerContext controllerContext = SetUpControllerContext();

            SetUpMapper();


            _userService = userServiceMoq.Object;

            _usersController = new UsersController(_userService, _mapper)
            {
                ControllerContext = controllerContext,
                Url = mockUrlHelper.Object
            };
        }

        private static ControllerContext SetUpControllerContext()
        {
            var response = new Mock<HttpResponse>();


            var testHeader = new Mock<IHeaderDictionary>();
            testHeader.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<StringValues>()));

            response.Setup(x => x.Headers).Returns(testHeader.Object);

            var httpContext = Mock.Of<HttpContext>(_ =>
                _.Response == response.Object
            );

            //Controller needs a controller context 
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };
            return controllerContext;
        }

        private static Mock<IUrlHelper> SetUpMockUrlHelper()
        {
            var mockUrlHelper = new Mock<IUrlHelper>(MockBehavior.Strict);

            mockUrlHelper.Setup(c => c.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("just a value");
            return mockUrlHelper;
        }

        private void SetUpMapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new UserProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

    }
}
