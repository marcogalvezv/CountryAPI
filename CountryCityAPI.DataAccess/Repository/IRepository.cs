using System.Linq.Expressions;
using CountryCityAPI.DataAccess.Entities;

namespace CountryCityAPI.DataAccess.Repository;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
    Task<Country?> GetCountryWithCitiesAsync(int id);
    Task AddCityAsync(City city);
    void DeleteCity(City city);
    Task<bool> CountryNameExistsAsync(string name, int? excludeId = null);
}
