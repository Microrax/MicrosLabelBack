using Azure.Storage.Blobs;
using MicrosLabel.Infrastructure.Configurations;
using MicrosLabel.Infrastructure.Extensions;
using System.Text.Json;

namespace MicrosLabel.Infrastructure.Services
{
    public sealed class AzureBlobContainerManager
    {
        private readonly AzureBlobContainerConfiguration _configuration;

        public AzureBlobContainerManager(AzureBlobContainerConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadAsync(string blobContainerName, string fileName, object message)
        {
            var client = GetOrCreateContainer(blobContainerName);

            var messageString = JsonSerializer.Serialize(message);
            return await UploadAsync(client, fileName, messageString);
        }

        private BlobContainerClient GetOrCreateContainer(string containerName)
        {
            var container = new BlobContainerClient(_configuration.ConnectionString, containerName);
            container.CreateIfNotExists();

            return container;
        }

        private static async Task<string> UploadAsync(BlobContainerClient client, string fileName, string message)
        {
            var blob = client.GetBlobClient(fileName);

            using (var stream = message.ToStream())
            {
                await blob.UploadAsync(stream);
            }

            return $"{client.Uri?.AbsoluteUri}/{fileName}";
        }
    }
}
