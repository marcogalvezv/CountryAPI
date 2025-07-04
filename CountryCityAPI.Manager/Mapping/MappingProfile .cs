using AutoMapper;
using CountryCityAPI.DataAccess.Entities;
using CountryCityAPI.Manager.Dtos;

namespace CountryCityAPI.Manager.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity -> DTO
        CreateMap<Country, CountryDto>()
            .ForMember(dest => dest.Cities, opt => opt.MapFrom(src => src.Cities));

        CreateMap<City, CityDto>();

        // DTO -> entidad
        CreateMap<CountryDto, Country>()
            .ForMember(dest => dest.Cities, opt => opt.Ignore());

        CreateMap<CityDto, City>();

        CreateMap<CountryCreateDto, Country>();

        CreateMap<CityCreateDto, City>();
    }
}
