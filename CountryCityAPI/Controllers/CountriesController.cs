using CountryCityAPI.Manager.Dtos;
using CountryCityAPI.Manager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CountryCityAPI.Host.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryManager _manager;

    public CountriesController(ICountryManager manager) => _manager = manager;

    //  Get All countries
    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _manager.GetAllCountriesAsync());

    //  Create a new Countries and its Cities
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CountryCreateDto countryDto)
    {
        if (await _manager.CountryNameExists(countryDto.Name))
        {
            return BadRequest("Country exists");
        }
        var created = await _manager.CreateCountryAsync(countryDto);
        return CreatedAtAction(nameof(Get), created);
    }

    //  Update Country and its Cities
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CountryDto countryDto)
    {   
        if (id != countryDto.Id)
        {
            return BadRequest("ID mismatch");
        }
        else if (await _manager.CountryNameExists(countryDto.Name, id))
        {
            return BadRequest("There is a country with that name");
        }
        return await _manager.UpdateCountryAsync(countryDto) ? Ok() : NotFound("Country not found");        
    }

    //  Delete a Country and its Cities
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _manager.DeleteCountryAsync(id) ? Ok("Country deleted") : NotFound("Country not found");
    }
}
