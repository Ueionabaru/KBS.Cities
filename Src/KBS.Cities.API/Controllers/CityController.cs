using KBS.Cities.Application.CQRS.Cities;
using KBS.Cities.Shared.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KBS.Cities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetPaginatedAsync([FromQuery] CityPaginationFilterDto filterDto)
        {
            var request = new GetPaginatedRequest { Filter = filterDto };
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] Guid? id = null)
        {
            var request = new GetDetailsRequest { Id = id ?? Guid.Empty };
            return Ok(await _mediator.Send(request));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> AddAsync([FromBody] CityEditDto dto)
        {
            var request = new AddOrUpdateRequest
            {
                Id = Guid.Empty,
                Data = dto
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> EditAsync([FromRoute] Guid id, [FromBody] CityEditDto dto)
        {
            var request = new AddOrUpdateRequest
            {
                Id = id,
                Data = dto
            };
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            var request = new DeleteRequest { Id = id };
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
