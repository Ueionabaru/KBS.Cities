using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using KBS.Cities.Shared.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using KBS.Cities.Application.Exceptions;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class AddOrUpdateRequest : IRequest
    {
        public Guid Id { get; set; }
        public CityEditDto Data { get; set; }

        public class AddOrUpdateRequestHandler : IRequestHandler<AddOrUpdateRequest>
        {
            private readonly IDbContext _appDbContext;

            public AddOrUpdateRequestHandler(IDbContext appDbContext) => _appDbContext = appDbContext;
            
            public async Task<Unit> Handle(AddOrUpdateRequest request, CancellationToken cancellationToken = default)
            {
                var cities = _appDbContext.Set<City>();
                var city = await cities.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (city is null && request.Id != Guid.Empty)
                    throw new NotFoundException($"City with id {request.Id} not found.");
                
                city = Map(city, request.Data);
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

            private City Map(City city, CityEditDto data)
            {
                city ??= new City();
                
                if (city.Name != data.Name)
                    city.Name = data.Name;

                if (city.Population != data.Population)
                    city.Population = data.Population;

                if (city.Established != data.Established)
                    city.Established = data.Established;

                return city;
            }
        }
    }
}
