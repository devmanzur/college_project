using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    public class CategoriesController : AuthorizedEndpoint
    {
        private readonly ICrudRepository<Category> _crudRepository;

        public CategoriesController(ICrudRepository<Category> crudRepository)
        {
            _crudRepository = crudRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ViewCategories()
        {
            var items = await _crudRepository.ListAll();
            return Ok(Envelope.Ok(items?.Select(x => new CategoryDto(x)).ToList()));
        }
    }
}