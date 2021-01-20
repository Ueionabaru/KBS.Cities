using KBS.Cities.Shared.DTO;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KBS.Cities.Application.Exceptions;
using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class GetDetailsRequest : IRequest<CityDto>
    {
        public Guid Id { get; set; }

        public class GetDetailsRequestHandler : IRequestHandler<GetDetailsRequest, CityDto>
        {
            private readonly IDbContext _appDbContext;

            public GetDetailsRequestHandler(IDbContext appDbContext) => _appDbContext = appDbContext;
            
            public async Task<CityDto> Handle(GetDetailsRequest request, CancellationToken cancellationToken = default)
            {
                var city = await _appDbContext.Set<City>().SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (city is null)
                    throw new NotFoundException($"City with id {request.Id} not found.");

                return Map(city);
            }
            
            private CityDto Map(City city)
            {
                return new()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Population = city.Population,
                    Established = city.Established
                };
            }
        }
    }
}
