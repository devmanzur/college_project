using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    public class CitiesController : AuthorizedEndpoint
    {
        private readonly ICrudRepository<City> _repository;

        public CitiesController(ICrudRepository<City> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _repository.ListAll();
            return Ok(Envelope.Ok(cities.Select(x => new CityDto(x)).ToList()));
        }

        [AllowAnonymous]
        [HttpGet("{id}/areas")]
        public async Task<IActionResult> GetAreas(int id)
        {
            var city = await _repository.Query().Include(x => x.Areas).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(Envelope.Ok(city.Areas.Select(x => new AreaDto(x)).ToList()));
        }
        // [AllowAnonymous]
        // [HttpPost]
        // public async Task<IActionResult> Create([FromForm] string name)
        // {
        //     var city = await _repository.Create(new City(name));
        //     return Ok(Envelope.Ok(new CityDto(city)));
        // }

        // [AllowAnonymous]
        // [HttpPost("{id}/areas")]
        // public async Task<IActionResult> Create(int id, [FromForm] AreaCreateDto dto)
        // {
        //     var city = await _repository.FindById(id);
        //     if (city == null)
        //     {
        //         return BadRequest(Envelope.Error("no such city with id: " + id));
        //     }
        //     city.Add(new Area(dto.Name));
        //     await _repository.Update(city);
        //     return Ok(Envelope.Ok(new CityDto(city)));
        // }
    }
}