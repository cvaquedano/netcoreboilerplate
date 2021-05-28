
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models;
using NetCoreWebApiBoilerPlate.Models.UserModel;
using NetCoreWebApiBoilerPlate.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AuthenticateResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Authenticate(AuthenticateRequestDto model)
        {
            var response = await _userService.AuthenticateAsync(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RegisterResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<RegisterResponseDto>> Register(RegisterRequestDto model)
        {
            var userEntity = _mapper.Map<User>(model);

            await _userService.AddAsync(userEntity);

            var userToReturn = _mapper.Map<RegisterResponseDto>(userEntity);

            return CreatedAtRoute("GetUser", new { userId = userToReturn.Id }, userToReturn);
        }

        [Authorize]
        [HttpPut("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser(Guid userId, UserForUpdateDto userForUpdateDto)
        {
            if (!await _userService.IsExistsAsync(userId))
            {
                return NotFound();
            }
            var userFromService = await _userService.GetByIdAsync(userId);

            _mapper.Map(userForUpdateDto, userFromService);

            await _userService.UpdateAsync(userFromService);


            return NoContent();
        }

        //[Authorize]
        //[HttpGet(Name = "GetAll")]
        //[ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAll([FromQuery] UsersRequestDto usersRequestDto)
        //{
        //    var usersFromServices = await _userService.GetAllAsync(usersRequestDto);
        //    var paginationMetadata = new
        //    {
        //        totalCount = usersFromServices.TotalCount,
        //        pageSize = usersFromServices.PageSize,
        //        currentPage = usersFromServices.CurrentPage,
        //        totalPages = usersFromServices.TotalPages,

        //    };

        //    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));
           

        //    var userToReturn = _mapper.Map<IEnumerable<UserResponseDto>>(usersFromServices);
        //    return Ok(userToReturn);


        //}

        [Authorize]
        [HttpGet("{userId:guid}", Name = "GetUser")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var userFromService = await _userService.GetByIdAsync(userId);

            if (userFromService == null)
            {
                return NotFound();
            }

            var authorToReturn = _mapper.Map<UserResponseDto>(userFromService);

            return Ok(authorToReturn);
        }

        [Authorize]
        [HttpGet(Name = "GetUserByToken")]
        [ProducesResponseType(typeof(UserResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserByToken()
        {
          

            if ((HttpContext.Items["User"] as User) == null)
            {
                return NotFound();
            }

            var authorToReturn = _mapper.Map<UserResponseDto>(HttpContext.Items["User"] as User);

            return Ok(authorToReturn);
        }

        [Authorize]
        [HttpDelete("{userId}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid userId)
        {
            var userFromService = await _userService.GetByIdAsync(userId);
            if (userFromService == null)
            {
                return NotFound();

            }
            await _userService.DeleteAsync(userFromService);


            return NoContent();

        }

    }
}
