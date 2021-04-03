
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Models.UserModel;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        public IActionResult Authenticate(AuthenticateRequestDto model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterResponseDto), StatusCodes.Status201Created)]
        public ActionResult<RegisterResponseDto> Register(RegisterRequestDto model)
        {
            var userEntity = _mapper.Map<User>(model);

            _userService.Register(userEntity);

            var userToReturn = _mapper.Map<RegisterResponseDto>(userEntity);

            return CreatedAtRoute("GetUser", new { userId = userToReturn.Id }, userToReturn);
        }

        [Authorize]
        [HttpPut("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateAuthor(Guid userId, UserForUpdateDto userForUpdateDto)
        {
            if (!_userService.IsEntityExist(userId))
            {
                return NotFound();
            }
            var userFromService = _userService.GetById(userId);

            _mapper.Map(userForUpdateDto, userFromService);

            _userService.Update(userFromService);


            return NoContent();
        }

        [Authorize]
        [HttpGet(Name = "GetAll")]
        [ProducesResponseType(typeof(UserBaseDto), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(UserBaseDto), StatusCodes.Status200OK)]
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

        [Authorize]
        [HttpDelete("{userId}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(Guid userId)
        {
            var userFromService = _userService.GetById(userId);
            if (userFromService == null)
            {
                return NotFound();

            }
            _userService.Delete(userFromService);


            return NoContent();

        }

    }
}
