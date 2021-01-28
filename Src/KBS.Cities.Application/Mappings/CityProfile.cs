using AutoMapper;
using KBS.Cities.Domain.Entities;
using KBS.Cities.Shared.DTO;

namespace KBS.Cities.Application.Mappings
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<City, CityEditDto>();
            CreateMap<CityEditDto, City>()
                .ForMember(c => c.Id, x => x.Ignore());
        }
    }
}
