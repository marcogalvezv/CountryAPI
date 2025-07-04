using CountryCityAPI.Manager.Dtos;

namespace CountryCityAPI.Manager.Services;

public interface ICountryManager
{
    Task<CountryDto?> GetCountryAsync(int id);
    Task<IEnumerable<CountryDto>> GetAllCountriesAsync();
    Task<CountryDto> CreateCountryAsync(CountryCreateDto country);
    Task<bool> UpdateCountryAsync(CountryDto country);
    Task<bool> DeleteCountryAsync(int id);
    Task<bool> CountryNameExists(string name, int? id = null);
}
