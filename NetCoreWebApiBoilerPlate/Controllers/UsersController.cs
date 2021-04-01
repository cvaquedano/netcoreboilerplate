
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Services;
using System;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public ActionResult<RegisterResponse> Register(RegisterRequest model)
        {
            var userEntity = _mapper.Map<User>(model);

            _userService.Register(userEntity);

            var userToReturn = _mapper.Map<RegisterResponse>(userEntity);

            return CreatedAtRoute("GetUser", new { userId = userToReturn.Id }, userToReturn);
        }

        [Authorize]
        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpGet("{userId:guid}", Name = "GetUser")]
        public IActionResult GetById(Guid userId)
        {
            var userFromService = _userService.GetById(userId);

            if (userFromService == null)
            {
                return NotFound();
            }

            var authorToReturn = _mapper.Map<UserBaseResponse>(userFromService);

            return Ok(authorToReturn);
        }

    }
}
