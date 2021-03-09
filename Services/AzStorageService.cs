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

        public async Task UploadData(string data)
        {
            Guid key = Guid.NewGuid();
            BlobContainerClient client = new BlobContainerClient(_configuration["Azure:Storage:ConnectionString"], _container);
            if (!Directory.Exists("./Storage"))
            {
                Directory.CreateDirectory("./Storage");
            }
            File.WriteAllText($"./Storage/blob-{key}.json", data);
            using FileStream stream = File.OpenRead($"./Storage/blob-{key}.json");
            await client.UploadBlobAsync($"blob-{key}.json", stream);
        }
    }
}