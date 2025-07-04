using AutoMapper;
using CountryCityAPI.DataAccess.Entities;
using CountryCityAPI.DataAccess.Repository;
using CountryCityAPI.Manager.Dtos;

namespace CountryCityAPI.Manager.Services;

public class CountryManager : ICountryManager
{
    private readonly IRepository<Country> _repository;
    private readonly IMapper _mapper;

    public CountryManager(IRepository<Country> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CountryDto> CreateCountryAsync(CountryCreateDto countryCreateDto)
    {
        var country = _mapper.Map<Country>(countryCreateDto);
        await _repository.AddAsync(country);
        await _repository.SaveChangesAsync();
        return _mapper.Map<CountryDto>(country);
    }

    public async Task<bool> DeleteCountryAsync(int id)
    {
        var country = await _repository.GetByIdAsync(id);

        if (country == null)
        {
            return false;
        }

        await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<CountryDto>> GetAllCountriesAsync()
    {
        var countries = await _repository.GetAllAsync(c => c.Cities);
        return _mapper.Map<IEnumerable<CountryDto>>(countries);
    }

    public async Task<CountryDto?> GetCountryAsync(int id)
    {
        var countries = await _repository.GetByIdAsync(id);
        return _mapper.Map<CountryDto>(countries);
    }

    public async Task<bool> UpdateCountryAsync(CountryDto countryDto)
    {
        var existingCountry = await _repository.GetCountryWithCitiesAsync(countryDto.Id);
        if (existingCountry == null) return false;

        _mapper.Map(countryDto, existingCountry);

        await UpdateCitiesAsync(existingCountry, countryDto.Cities);
        await _repository.SaveChangesAsync();

        return true;
    }

    private async Task UpdateCitiesAsync(Country country, List<CityDto> cityDtos)
    {
        var citiesToRemove = country.Cities
            .Where(c => !cityDtos.Any(dto => dto.Id == c.Id))
            .ToList();

        foreach (var city in citiesToRemove)
        {
            _repository.DeleteCity(city);
        }

        foreach (var cityDto in cityDtos)
        {
            if (cityDto.Id > 0)
            {
                var existingCity = country.Cities.FirstOrDefault(c => c.Id == cityDto.Id);
                if (existingCity != null)
                {
                    _mapper.Map(cityDto, existingCity);
                }
            }
            else
            {
                var newCity = _mapper.Map<City>(cityDto);
                newCity.CountryId = country.Id;
                await _repository.AddCityAsync(newCity);
            }
        }
    }

    public async Task<bool> CountryNameExists(string name, int? id = null)
    {
        return await _repository.CountryNameExistsAsync(name, id);
    }
}
