using KBS.Cities.Shared.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class GetPaginatedRequest : IRequest<PaginatedDto<CityDto>>
    {
        public PaginationFiltersDto Filters { get; set; }

        public class GetPaginatedRequestHandler : IRequestHandler<GetPaginatedRequest, PaginatedDto<CityDto>>
        {
            private readonly IDbContext _appDbContext;

            public GetPaginatedRequestHandler(IDbContext appDbContext) => _appDbContext = appDbContext;
            
            public async Task<PaginatedDto<CityDto>> Handle(GetPaginatedRequest request, CancellationToken cancellationToken = default)
            {
                var query = _appDbContext.Set<City>().Where(_ => true);
                var total = await query.CountAsync(cancellationToken);

                var filters = request.Filters;
                var totalPages = (int)Math.Ceiling(total / (double)filters.PageSize);
                if (filters.PageIndex > totalPages) filters.PageIndex = totalPages;
                
                query = FilterQuery(query, filters);

                var cities = await query.Select(c => new City
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Population = c.Population,
                        Established = c.Established
                    })
                    .ToListAsync(cancellationToken);
                var data = Map(cities);
                
                return new PaginatedDto<CityDto>
                {
                    Data = data.ToList(),
                    TotalItems = total,
                    PageIndex = filters.PageIndex,
                    PageSize = filters.PageSize
                };
            }
            
            private IEnumerable<CityDto> Map(IEnumerable<City> cities)
            {
                return cities.Select(c => new CityDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Population = c.Population,
                        Established = c.Established
                    })
                    .ToList();
            }

            private IQueryable<City> FilterQuery(IQueryable<City> query, PaginationFiltersDto filters)
            {
                var dateFrom = DateTime.ParseExact(filters.DateFrom, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
                var dateTo = DateTime.ParseExact(filters.DateTo, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

                query = query.Skip((filters.PageIndex - 1) * filters.PageSize)
                    .Take(filters.PageSize);
                
                if (!string.IsNullOrEmpty(filters.Name))
                    query = query.Where(c => c.Name != null);

                if (filters.PopulationFrom != int.MaxValue)
                    query = query.Where(c => c.Population >= filters.PopulationFrom);

                if (filters.PopulationTo != int.MaxValue)
                    query = query.Where(c => c.Population <= filters.PopulationTo);

                if (dateFrom != DateTime.MinValue)
                    query = query.Where(c => c.Established >= dateFrom);

                if (dateTo != DateTime.MaxValue)
                    query = query.Where(c => c.Established <= dateTo);

                return query;
            }
        }
    }
}
