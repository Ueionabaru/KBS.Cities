using KBS.Cities.Shared.DTO;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using KBS.Cities.Application.Exceptions;
using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class GetDetailsRequest : IRequest<CityDto>
    {
        public Guid Id { get; set; }

        public class GetDetailsRequestHandler : IRequestHandler<GetDetailsRequest, CityDto>
        {
            private readonly IDbContext _appDbContext;
            private readonly IMapper _mapper;

            public GetDetailsRequestHandler(IDbContext appDbContext, IMapper mapper)
            {
                _appDbContext = appDbContext;
                _mapper = mapper;
            }

            public async Task<CityDto> Handle(GetDetailsRequest request, CancellationToken cancellationToken = default)
            {
                var city = await _appDbContext.Set<City>().SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (city is null)
                    throw new NotFoundException($"City with id {request.Id} not found.");

                return _mapper.Map<CityDto>(city);
            }
        }
    }
}
