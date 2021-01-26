using FluentAssertions;
using KBS.Cities.Application.CQRS.Cities;
using KBS.Cities.Domain.Entities;
using KBS.Cities.Shared;
using KBS.Cities.Shared.DTO;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace KBS.Cities.Application.IntegrationTests.Cities
{
    using static TestSetup;

    public class GetPaginatedRequestTests : BaseTest
    {
        [Test]
        public async Task ShouldReturnDefaultPagination_WithDefaultFilter()
        {
            // Arrange & Act
            const int defaultPageIndex = Constants.Pagination.DefaultPageIndex;
            const int defaultPageSize = Constants.Pagination.DefaultPageSize;
            var expectedTotalItemsCount = await CountAsync<City>();

            var result = await ArrangeAndAct();

            // Assert
            result.PageIndex.Should().Be(defaultPageIndex, $"{defaultPageIndex} is default page number for an empty request.");
            result.PageSize.Should().Be(defaultPageSize, $"{defaultPageSize} is default page size for an empty request.");
            result.TotalItems.Should().Be(expectedTotalItemsCount);
            result.Data.Should().BeInDescendingOrder(c => c.Population);
        }

        [Test]
        public async Task ShouldReturnLastPagePagination_WithPageIndexBeingThree()
        {
            // Arrange & Act
            const int pageIndex = 3;
            var filter = new CityPaginationFilterDto
            {
                PageIndex = pageIndex
            };

            var result = await ArrangeAndAct(filter);

            // Assert
            result.PageIndex.Should().Be(result.TotalPages, "If the request's page index is greater than total pages, resulting index is a last page");
            result.PageIndex.Should().NotBe(pageIndex, $"{pageIndex} is greater than total pages.");
            result.TotalPages.Should().BeLessThan(pageIndex);
        }

        [TestCase(nameof(CityDto.Name))]
        [TestCase(nameof(CityDto.Population))]
        [TestCase(nameof(CityDto.Established))]
        public async Task ShouldReturnPagination_WithOrderAscendingBy(string orderBy)
        {
            // Arrange & Act
            var filter = new CityPaginationFilterDto
            {
                OrderBy = orderBy,
                OrderDirection = Shared.Enums.OrderDirection.Ascending
            };
            var result = await ArrangeAndAct(filter);

            // Assert
            switch (orderBy)
            {
                case nameof(CityDto.Name):
                    result.Data.Should().BeInAscendingOrder(c => c.Name);
                    break;
                case nameof(CityDto.Population):
                    result.Data.Should().BeInAscendingOrder(c => c.Population);
                    break;
                case nameof(CityDto.Established):
                    result.Data.Should().BeInAscendingOrder(c => c.Established);
                    break;
            }
        }

        [TestCase(nameof(CityDto.Name))]
        [TestCase(nameof(CityDto.Population))]
        [TestCase(nameof(CityDto.Established))]
        public async Task ShouldReturnPagination_WithOrderDescendingBy(string orderBy)
        {
            // Arrange & Act
            var filter = new CityPaginationFilterDto
            {
                OrderBy = orderBy,
                OrderDirection = Shared.Enums.OrderDirection.Descending
            };
            var result = await ArrangeAndAct(filter);

            // Assert
            switch (orderBy)
            {
                case nameof(CityDto.Name):
                    result.Data.Should().BeInDescendingOrder(c => c.Name);
                    break;
                case nameof(CityDto.Population):
                    result.Data.Should().BeInDescendingOrder(c => c.Population);
                    break;
                case nameof(CityDto.Established):
                    result.Data.Should().BeInDescendingOrder(c => c.Established);
                    break;
            }
        }

        [TestCase(1700, 1800)]
        [TestCase(1800, 1900)]
        [TestCase(1900, 2000)]
        public async Task ShouldReturnPagination_WithFilterEstablished(int dateFrom, int dateTo)
        {
            // Arrange & Act
            var after = new DateTime(dateFrom, 1, 1);
            var before = new DateTime(dateTo, 1, 1);
            var filter = new CityPaginationFilterDto
            {
                DateFrom = new DateTime(dateFrom, 1, 1),
                DateTo = new DateTime(dateTo, 1, 1)
            };
            var result = await ArrangeAndAct(filter);

            // Assert
            result.Data.ForEach(c =>
            {
                c.Established.Should().BeOnOrAfter(after);
                c.Established.Should().BeOnOrBefore(before);
            });
        }

        [TestCase("sh")]
        public async Task ShouldReturnPagination_WithFilterName(string namePart)
        {
            // Arrange & Act
            var filter = new CityPaginationFilterDto { Name = namePart };
            var result = await ArrangeAndAct(filter);

            // Assert
            result.Data.ForEach(c =>
            {
                c.Name.ToLowerInvariant().Should().Contain(namePart.ToLowerInvariant());
            });
        }

        [TestCase(0, 100_000)]
        [TestCase(100_000, 1_000_000)]
        public async Task ShouldReturnPagination_WithFilterPopulation(int populationFrom, int populationTo)
        {
            // Arrange & Act
            var filter = new CityPaginationFilterDto
            {
                PopulationFrom = populationFrom,
                PopulationTo = populationTo
            };
            var result = await ArrangeAndAct(filter);

            // Assert
            result.Data.ForEach(c =>
            {
                c.Population.Should().BeGreaterOrEqualTo(populationFrom);
                c.Population.Should().BeLessThan(populationTo);
            });
        }

        private async Task<PaginatedDto<CityDto>> ArrangeAndAct(CityPaginationFilterDto filter = null)
        {
            // Arrange
            var query = new GetPaginatedRequest { Filter = filter };

            // Act
            var result = await SendAsync(query);

            return result;
        }
    }
}
