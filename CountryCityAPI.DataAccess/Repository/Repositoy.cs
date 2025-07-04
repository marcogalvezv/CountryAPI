using System.Linq.Expressions;
using CountryCityAPI.DataAccess.Context;
using CountryCityAPI.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CountryCityAPI.DataAccess.Repository;

public class Repository<T>(AppDbContext context) : IRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = context.Set<T>();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }
    public async Task<T?> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity) => await context.Set<T>().AddAsync(entity);

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    public Task UpdateAsync(T entity) => Task.FromResult(context.Entry(entity).State = EntityState.Modified);

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) context.Set<T>().Remove(entity);
    }
    public async Task<Country?> GetCountryWithCitiesAsync(int id)
    {
        var country = await context.Countries
            .Include(c => c.Cities)
            .FirstOrDefaultAsync(c => c.Id == id);

        return country;
    }

    public async Task AddCityAsync(City city) => await context.Cities.AddAsync(city);

    public void DeleteCity(City city) => context.Cities.Remove(city);

    public async Task<bool> CountryNameExistsAsync(string name, int? excludeId = null)
    {
        var query = context.Countries
            .Where(c => c.Name.ToLower() == name.ToLower());

        if (excludeId.HasValue)
        {
            query = query.Where(c => c.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }
}
