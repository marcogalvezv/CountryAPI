namespace CountryCityAPI.Manager.Dtos;

public class CountryCreateDto
{
    public string Name { get; set; }
    public List<CityCreateDto> Cities { get; set; } = new List<CityCreateDto>();
}
