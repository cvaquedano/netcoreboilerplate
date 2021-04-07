using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApiBoilerPlate.Entities;
using NetCoreWebApiBoilerPlate.Helpers;
using NetCoreWebApiBoilerPlate.Models.MasterDetailModel;
using NetCoreWebApiBoilerPlate.Services;
using Newtonsoft.Json;

namespace NetCoreWebApiBoilerPlate.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDetailController : ControllerBase
    {
        private readonly IMasterDetailService _service;
        private readonly IMapper _mapper;

        public MasterDetailController(IMapper mapper, IMasterDetailService service)
        {
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            _service = service ?? throw new ArgumentException(nameof(service));
        }

        [HttpGet(Name = "GetAllMasterDetail")]
        [ProducesResponseType(typeof(MasterDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MasterDetailResponseDto>>> GetAll([FromQuery] MasterDetailRequestDto requestDto)
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


            var userToReturn = _mapper.Map<IEnumerable<MasterDetailResponseDto>>(etitiesFromServices);
            return Ok(userToReturn);
        }

        [HttpGet("{id:guid}", Name = "GetMasterDetailById")]
        [ProducesResponseType(typeof(MasterDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MasterDetailResponseDto>> GetById(Guid id)
        {
            var entityfromService = await _service.GetByIdAsync(id);

            if (entityfromService == null)
            {
                return NotFound();
            }

            var dtoToReturn = _mapper.Map<MasterDetailResponseDto>(entityfromService);

            return Ok(dtoToReturn);
        }

        // PUT: api/MasterDetail/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutMasterDetailEntity(Guid id, MasterDetailForUpdateDto forUpdateDto)
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

        // POST: api/MasterDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MasterDetailResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<MasterDetailResponseDto>> PostMasterDetail(MasterDetailForCreateDto forCreateDto)
        {

            var entity = _mapper.Map<MasterDetailEntity>(forCreateDto);

            await _service.AddAsync(entity);

            var toReturn = _mapper.Map<MasterDetailResponseDto>(entity);

            return CreatedAtRoute("GetMasterDetailById", new { id = toReturn.Id }, toReturn);

        }

        // DELETE: api/MasterDetail/5
        [HttpDelete("{id}", Name = "DeleteMasterDetail")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMasterDetailEntity(Guid id)
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
