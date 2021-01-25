using AutoMapper;
using KBS.Cities.Application.Interfaces;
using KBS.Cities.Domain.Entities;
using KBS.Cities.Shared.DTO;
using KBS.Cities.Shared.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KBS.Cities.Application.CQRS.Cities
{
    public class GetPaginatedRequest : IRequest<PaginatedDto<CityDto>>
    {
        public CityPaginationFilterDto Filter { get; set; }

        public class GetPaginatedRequestHandler : IRequestHandler<GetPaginatedRequest, PaginatedDto<CityDto>>
        {
            private readonly IDbContext _appDbContext;
            private readonly IMapper _mapper;

            public GetPaginatedRequestHandler(IDbContext appDbContext, IMapper mapper)
            {
                _appDbContext = appDbContext;
                _mapper = mapper;
            }

            public async Task<PaginatedDto<CityDto>> Handle(GetPaginatedRequest request, CancellationToken cancellationToken = default)
            {
                var filter = request.Filter;
                var query = _appDbContext.Set<City>().Where(_ => true);
                var total = await query.CountAsync(cancellationToken);

                var totalPages = (int)Math.Ceiling(total / (double)filter.PageSize);
                if (filter.PageIndex > totalPages) filter.PageIndex = totalPages;

                query = OrderQuery(query, filter);
                query = FilterQuery(query, filter);
                query = PaginateQuery(query, filter);

                var cities = await query.Select(c => new City
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Population = c.Population,
                        Established = c.Established
                    })
                    .ToListAsync(cancellationToken);

                var data = _mapper.Map<List<CityDto>>(cities);
                return new PaginatedDto<CityDto>
                {
                    Data = data,
                    TotalItems = total,
                    PageIndex = filter.PageIndex,
                    PageSize = filter.PageSize
                };
            }

            private static IQueryable<City> OrderQuery(IQueryable<City> query, FilterDto filter)
            {
                query = filter.OrderDirection switch
                {
                    OrderDirection.Ascending => filter.OrderBy switch
                    {
                        nameof(City.Name) => query.OrderBy(c => c.Name),
                        nameof(City.Population) => query.OrderBy(c => c.Population),
                        nameof(City.Established) => query.OrderBy(c => c.Established),
                        _ => query
                    },
                    OrderDirection.Descending => filter.OrderBy switch
                    {
                        nameof(City.Name) => query.OrderBy(c => c.Name),
                        nameof(City.Population) => query.OrderBy(c => c.Population),
                        nameof(City.Established) => query.OrderBy(c => c.Established),
                        _ => query
                    },
                    _ => throw new ArgumentOutOfRangeException(nameof(OrderDirection))
                };
                return query;
            }

            private static IQueryable<City> FilterQuery(IQueryable<City> query, CityPaginationFilterDto filter)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                    query = query.Where(c => c.Name != null);

                if (filter.PopulationFrom != int.MaxValue)
                    query = query.Where(c => c.Population >= filter.PopulationFrom);

                if (filter.PopulationTo != int.MaxValue)
                    query = query.Where(c => c.Population <= filter.PopulationTo);

                if (filter.DateFrom != DateTime.MinValue)
                    query = query.Where(c => c.Established >= filter.DateFrom);

                if (filter.DateTo != DateTime.MaxValue)
                    query = query.Where(c => c.Established <= filter.DateTo);

                return query;
            }

            private static IQueryable<City> PaginateQuery(IQueryable<City> query, PaginationFilterDto filters)
            {
                query = query
                    .Skip((filters.PageIndex - 1) * filters.PageSize)
                    .Take(filters.PageSize);

                return query;
            }
        }
    }
}
