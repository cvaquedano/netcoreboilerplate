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
    [Route("api/master/{masterId}/[controller]")]
    [ApiController]
    public class MasterDetailController : ControllerBase
    {
        private readonly IMasterDetailService _service;
        private readonly IExampleMasterService _masterservice;
        private readonly IMapper _mapper;

        public MasterDetailController(IMapper mapper, IMasterDetailService service, IExampleMasterService masterservice)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _masterservice = masterservice ?? throw new ArgumentNullException(nameof(masterservice));
        }

        [HttpGet(Name = "GetDetailForMaster")]
        [ProducesResponseType(typeof(MasterDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MasterDetailResponseDto>>> GetAllForMaster(Guid masterId, [FromQuery] MasterDetailRequestDto requestDto)
        {

            if (!await _masterservice.IsExistsAsync(masterId))
            {
                return NotFound();
            }

            var entitiesFromServices = await _service.GetAllForMasterAsync(masterId,requestDto);

            var paginationMetadata = new
            {
                totalCount = entitiesFromServices.TotalCount,
                pageSize = entitiesFromServices.PageSize,
                currentPage = entitiesFromServices.CurrentPage,
                totalPages = entitiesFromServices.TotalPages,

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetadata));


            var objectToReturn = _mapper.Map<IEnumerable<MasterDetailResponseDto>>(entitiesFromServices);
            return Ok(objectToReturn);
        }

        [HttpGet("{id:guid}", Name = "GetMasterDetailById")]
        [ProducesResponseType(typeof(MasterDetailResponseDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<MasterDetailResponseDto>> GetById(Guid masterId, Guid id)
        {
            if (! await _masterservice.IsExistsAsync(masterId))
            {
                return NotFound();
            }
            var entityfromService = await _service.GetByIdForMasterAsync(masterId ,id);

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
        public async Task<IActionResult> PutMasterDetailEntity(Guid masterId, Guid id, MasterDetailForUpdateDto forUpdateDto)
        {
            if (! await _masterservice.IsExistsAsync(masterId))
            {
                return NotFound();
            }

            var entityFromService = await _service.GetByIdForMasterAsync(masterId,id);

            _mapper.Map(forUpdateDto, entityFromService);

            await _service.UpdateAsync(entityFromService);


            return NoContent();

        }

        // POST: api/MasterDetail
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(MasterDetailResponseDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<MasterDetailResponseDto>> PostMasterDetail(Guid masterId, MasterDetailForCreateDto forCreateDto)
        {
            if (!await _masterservice.IsExistsAsync(masterId))
            {
                return NotFound();
            }

            var entity = _mapper.Map<MasterDetailEntity>(forCreateDto);

            entity.ExampleMasterEntityId = masterId;

            await _service.AddAsync(entity);

            var objectToReturn = _mapper.Map<MasterDetailResponseDto>(entity);

            return CreatedAtRoute("GetMasterDetailById", new { masterId, id = objectToReturn.Id }, objectToReturn);

        }

        // DELETE: api/MasterDetail/5
        [HttpDelete("{id}", Name = "DeleteMasterDetail")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMasterDetailEntity(Guid masterId, Guid id)
        {
            if (!await _masterservice.IsExistsAsync(masterId))
            {
                return NotFound();
            }

            var entityfromService = await _service.GetByIdForMasterAsync(masterId, id);
            if (entityfromService == null)
            {
                return NotFound();

            }
            await _service.DeleteAsync(entityfromService);


            return NoContent();
        }


    }
}
