using System.ComponentModel.DataAnnotations;

namespace CountryCityAPI.DataAccess.Entities;

public class Country
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public ICollection<City> Cities { get; set; } = new List<City>();
}
