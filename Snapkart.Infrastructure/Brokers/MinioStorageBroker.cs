using System;
using System.IO;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Minio;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Infrastructure.Brokers
{
    public class MinioStorageBroker : IStorageServiceBroker
    {
        // public async Task<Result<string>> UploadImage(IFormFile file)
        // {
        //     return Result.Success("https://uifaces.co/our-content/donated/vIqzOHXj.jpg");
        // }

        private readonly string _server;
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _bucketName;
        private readonly string _localImageDirectory;
        private readonly IConfiguration _configuration;
        private MinioClient s3Client;
        private const string ContentType = "image/jpeg";

        public MinioStorageBroker(IConfiguration configuration)
        {
            _configuration = configuration;
            _server = configuration.GetSection("MinioConfigurationSettings").GetSection("Server").Value;
            _accessKey = configuration.GetSection("MinioConfigurationSettings").GetSection("AccessKey").Value;
            _secretKey = configuration.GetSection("MinioConfigurationSettings").GetSection("SecretKey").Value;
            _bucketName = configuration.GetSection("MinioConfigurationSettings").GetSection("BucketName").Value;
            _localImageDirectory = configuration.GetSection("MinioConfigurationSettings").GetSection("LocalImageLocation").Value;
            s3Client = new MinioClient(_server, _accessKey, _secretKey);
        }

        public async Task<Result<string>> UploadImage(IFormFile image)
        {
            try
            {
                var imageId = await Run(image, _bucketName);
                return string.IsNullOrEmpty(imageId) ? throw new Exception() : imageId;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload image", ex);
            }
        }

        private async Task<string> Run(IFormFile file, string bucket)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;
            await SetupBucket(s3Client, bucket);
            var path = await WriteFile(file, fileName);
            await s3Client.PutObjectAsync(bucket, fileName, path, ContentType);
            File.Delete(path);
            return fileName;
        }

        private  async Task<string> WriteFile(IFormFile file, string fileName)
        {
            var imagePath = _localImageDirectory;
            var path = Path.Combine(Directory.GetCurrentDirectory(), imagePath, fileName);
            await using var bits = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(bits);
            bits.Close();
            return path;
        }

        private static async Task SetupBucket(MinioClient client, string bucket)
        {
            var found = await client.BucketExistsAsync(bucket);
            if (!found)
            {
                await client.MakeBucketAsync(bucket);
            }
        }
    }
}