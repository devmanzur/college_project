using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Contract;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : AuthorizedEndpoint
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> ViewCategories()
        {
            return Ok(Envelope.Ok(await _categoryService.GetAll()));
        }
    }
}