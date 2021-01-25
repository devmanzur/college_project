using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;

namespace Snapkart.Domain.Interfaces
{
    public interface IStorageServiceBroker
    {
        public Task<Result<string>> UploadImage(IFormFile file);
    }
}