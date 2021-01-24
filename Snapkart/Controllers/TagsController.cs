using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    public class TagsController : AuthorizedEndpoint
    {
        private readonly ICrudRepository<Tag> _repository;

        public TagsController(ICrudRepository<Tag> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _repository.ListAll();
            return Ok(Envelope.Ok(tags.Select(x => new TagDto(x)).ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string name)
        {
            var tag = await _repository.Create(new Tag(name));
            return Ok(Envelope.Ok(tag));
        }
    }
}