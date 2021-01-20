using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using KBS.Cities.Application.Exceptions;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class DeleteRequest : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteRequestHandler : IRequestHandler<DeleteRequest>
        {
            private readonly IDbContext _appDbContext;

            public DeleteRequestHandler(IDbContext appDbContext) => _appDbContext = appDbContext;

            public async Task<Unit> Handle(DeleteRequest request, CancellationToken cancellationToken = default)
            {
                var cities = _appDbContext.Set<City>();
                var city = await cities.SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (city is null)
                    throw new NotFoundException($"City with id {request.Id} not found.");

                cities.Remove(city);
                await _appDbContext.SaveChangesAsync(cancellationToken);

                return default;
            }
        }
    }
}
