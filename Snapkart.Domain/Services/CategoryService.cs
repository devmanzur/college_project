using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICrudRepository<Category> _crudRepository;

        public CategoryService(ICrudRepository<Category> crudRepository)
        {
            _crudRepository = crudRepository;
        }
        public async Task<List<CategoryDto>> GetAll()
        {
            var items = await _crudRepository.ListAll();
            return items?.Select(x => new CategoryDto(x)).ToList();
        }
    }
}