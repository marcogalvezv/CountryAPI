using System.ComponentModel.DataAnnotations;

namespace CountryCityAPI.DataAccess.Entities;

public class City
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public int Population { get; set; }

    public int CountryId { get; set; }

    public Country Country { get; set; }
}
