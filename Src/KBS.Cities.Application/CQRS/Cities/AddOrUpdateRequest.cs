using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using KBS.Cities.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using KBS.Cities.Application.Exceptions;
using AutoMapper;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class AddOrUpdateRequest : IRequest
    {
        public Guid Id { get; set; }
        public CityEditDto Data { get; set; }

        public class AddOrUpdateRequestHandler : IRequestHandler<AddOrUpdateRequest>
        {
            private readonly IDbContext _appDbContext;
            private readonly IMapper _mapper;

            public AddOrUpdateRequestHandler(IDbContext appDbContext, IMapper mapper) 
            { 
                _appDbContext = appDbContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(AddOrUpdateRequest request, CancellationToken cancellationToken = default)
            {
                var cities = _appDbContext.Set<City>();
                var city = await cities.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (city is null && request.Id != Guid.Empty)
                    throw new NotFoundException($"City with id {request.Id} not found.");

                city ??= new City();
                city = _mapper.Map<City>(request.Data);
                if (request.Id == Guid.Empty)
                {
                    cities.Add(city);
                    await _appDbContext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    cities.Update(city);
                    await _appDbContext.SaveChangesAsync(cancellationToken);
                }
                
                return default;
            }
        }
    }
}
