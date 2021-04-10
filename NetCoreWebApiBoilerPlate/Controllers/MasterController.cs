using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Domain.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.MasterModel;
using NetCoreWebApiBoilerPlate.Services;
using Newtonsoft.Json;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
       
        private readonly IExampleMasterService _service;
        private readonly IMapper _mapper;

        public MasterController(IMapper mapper, IExampleMasterService service)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _service = service ?? throw new ArgumentException(nameof(service));
        }

        [HttpGet(Name = "GetAllMaster")]
        [ProducesResponseType(typeof(MasterResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MasterResponseDto>>> GetAll([FromQuery] MasterRequestDto requestDto)
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


            var userToReturn = _mapper.Map<IEnumerable<MasterResponseDto>>(etitiesFromServices);
            return Ok(userToReturn);
        }

        [HttpGet("{id:guid}", Name = "GetMasterById")]
        [ProducesResponseType(typeof(MasterResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MasterResponseDto>> GetById(Guid id)
        {
            var entityfromService = await _service.GetByIdAsync(id);

            if (entityfromService == null)
            {
                return NotFound();
            }

            var dtoToReturn = _mapper.Map<MasterResponseDto>(entityfromService);

            return Ok(dtoToReturn);
        }

        
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutExampleMasterEntity(Guid id, MasterForUpdateDto  forUpdateDto)
        {
            if (! await _service.IsExistsAsync(id))
            {
                return NotFound();
            }
            var entityFromService = await _service.GetByIdAsync(id);

            _mapper.Map(forUpdateDto, entityFromService);

            await _service.UpdateAsync(entityFromService);


            return NoContent();
        }

        // POST: api/Master
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MasterResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<MasterResponseDto>> PostExampleMaster(MasterForCreateDto forCreateDto)
        {

            var entity = _mapper.Map<ExampleMasterEntity>(forCreateDto);

            await _service.AddAsync(entity);

            var toReturn = _mapper.Map<MasterResponseDto>(entity);

            return CreatedAtRoute("GetMasterById", new { id = toReturn.Id }, toReturn);
           
        }

        // DELETE: api/Master/5
        [HttpDelete("{id}", Name = "DeleteMaster")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteExampleMasterEntity(Guid id)
        {

            var entityfromService = await  _service.GetByIdAsync(id);
            if (entityfromService == null)
            {
                return NotFound();

            }
            await _service.DeleteAsync(entityfromService);


            return NoContent();
        }

      
    }
}
