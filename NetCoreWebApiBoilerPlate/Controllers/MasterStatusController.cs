using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.MasterStatusModel;
using NetCoreWebApiBoilerPlate.Services;
using Newtonsoft.Json;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterStatusController : ControllerBase
    {
        private readonly IMasterStatusService _service;
        private readonly IMapper _mapper;

        public MasterStatusController(IMapper mapper, IMasterStatusService service)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _service = service ?? throw new ArgumentException(nameof(service));
        }

        [HttpGet(Name = "GetAllMasterStatus")]
        [ProducesResponseType(typeof(MasterStatusResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MasterStatusResponseDto>>> GetAll([FromQuery] MasterStatusRequestDto requestDto)
        {
            var etitiesFromServices = await _service.GetAllAsync(requestDto);
            var paginationMetadata = new
            {
                totalCount = etitiesFromServices.TotalCount,
                pageSize = etitiesFromServices.PageSize,
                currentPage = etitiesFromServices.CurrentPage,
                totalPages = etitiesFromServices.TotalPages,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));


            var userToReturn = _mapper.Map<IEnumerable<MasterStatusResponseDto>>(etitiesFromServices);
            return Ok(userToReturn);
        }

        [HttpGet("{id:guid}", Name = "GetMasterStatusById")]
        [ProducesResponseType(typeof(MasterStatusResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MasterStatusResponseDto>> GetById(Guid id)
        {
            var entityfromService = await _service.GetByIdAsync(id);

            if (entityfromService == null)
            {
                return NotFound();
            }

            var dtoToReturn = _mapper.Map<MasterStatusResponseDto>(entityfromService);

            return Ok(dtoToReturn);
        }

        // PUT: api/MasterStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutMasterStatusEntity(Guid id, MasterStatusForUpdateDto forUpdateDto)
        {
            if (!await _service.IsExistsAsync(id))
            {
                return NotFound();
            }
            var entityFromService = await _service.GetByIdAsync(id);

            _mapper.Map(forUpdateDto, entityFromService);

            await _service.UpdateAsync(entityFromService);


            return NoContent();
        }

        // POST: api/MasterStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MasterStatusResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<MasterStatusResponseDto>> PostMasterStatus(MasterStatusForCreateDto forCreateDto)
        {

            var entity = _mapper.Map<MasterStatusEntity>(forCreateDto);

            await _service.AddAsync(entity);

            var toReturn = _mapper.Map<MasterStatusResponseDto>(entity);

            return CreatedAtRoute("GetMasterStatusById", new { id = toReturn.Id }, toReturn);

        }

        // DELETE: api/MasterStatus/5
        [HttpDelete("{id}", Name = "DeleteMasterStatus")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMasterStatusEntity(Guid id)
        {

            var fromService = await _service.GetByIdAsync(id);
            if (fromService == null)
            {
                return NotFound();

            }
            await _service.DeleteAsync(fromService);


            return NoContent();
        }


    }
}
