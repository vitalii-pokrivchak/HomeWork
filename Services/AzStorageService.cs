using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace HomeWork.Services
{
    public class AzStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly string _container = "homework";
        public AzStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task UploadData(Stream stream)
        {
            Guid key = Guid.NewGuid();
            BlobContainerClient client = new BlobContainerClient(_configuration.GetConnectionString("AzureStorage"), _container);
            await client.UploadBlobAsync($"users-{key}.json", stream);
        }
    }
}