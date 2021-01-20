using KBS.Cities.Application.CQRS.Cities;
using KBS.Cities.Shared.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KBS.Cities.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet, Route("")]
        public async Task<IActionResult> GetPaginatedAsync([FromQuery] PaginationFiltersDto filtersDto)
        {
            var request = new GetPaginatedRequest { Filters = filtersDto };
            var response = await Mediator.Send(request);
            return Ok(response);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetDetailsAsync([FromRoute] Guid? id = null)
        {
            var request = new GetDetailsRequest { Id = id ?? Guid.Empty };
            return Ok(await Mediator.Send(request));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> AddAsync([FromBody] CityEditDto dto)
        {
            var request = new AddOrUpdateRequest
            {
                Id = Guid.Empty,
                Data = dto
            };
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpPut, Route("{id}")]
        public async Task<IActionResult> EditAsync([FromRoute] Guid id, [FromBody] CityEditDto dto)
        {
            var request = new AddOrUpdateRequest
            {
                Id = id,
                Data = dto
            };
            await Mediator.Send(request);
            return NoContent();
        }

        [HttpDelete, Route("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            var request = new DeleteRequest { Id = id };
            await Mediator.Send(request);
            return NoContent();
        }
    }
}
