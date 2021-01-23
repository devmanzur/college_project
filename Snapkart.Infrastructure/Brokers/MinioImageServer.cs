using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Infrastructure.Brokers
{
    public class MinioImageServer : IImageServerBroker
    {
        public async Task<Result<string>> UploadImage(IFormFile file)
        {
            return Result.Success("https://uifaces.co/our-content/donated/vIqzOHXj.jpg");
        }
    }
}