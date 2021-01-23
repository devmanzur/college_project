using System.Collections.Generic;
using System.Threading.Tasks;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;

namespace Snapkart.Domain.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAll();
    }
}