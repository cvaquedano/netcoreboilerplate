
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
        public IActionResult Authenticate(AuthenticateRequestDto model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public ActionResult<RegisterResponseDto> Register(RegisterRequestDto model)
        {
            var userEntity = _mapper.Map<User>(model);

            _userService.Register(userEntity);

            var userToReturn = _mapper.Map<RegisterResponseDto>(userEntity);

            return CreatedAtRoute("GetUser", new { userId = userToReturn.Id }, userToReturn);
        }

        [Authorize]
        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll([FromQuery] UsersRequestDto usersRequestDto)
        {
            var usersFromServices = _userService.GetAll(usersRequestDto);
            var paginationMetadata = new
            {
                totalCount = usersFromServices.TotalCount,
                pageSize = usersFromServices.PageSize,
                currentPage = usersFromServices.CurrentPage,
                totalPages = usersFromServices.TotalPages,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
           

            var userToReturn = _mapper.Map<IEnumerable<UserBaseDto>>(usersFromServices);
            return Ok(userToReturn);


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

            var authorToReturn = _mapper.Map<UserBaseDto>(userFromService);

            return Ok(authorToReturn);
        }

    }
}
