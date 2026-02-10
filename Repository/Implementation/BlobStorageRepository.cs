using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BlobStorageDemo.Repository
{
    public class BlobStorageRepo : IBlobStorageRepo
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageRepo(IConfiguration configuration)
        {
            var connectionString = configuration["AzureBlob:ConnectionString"];
            var containerName =configuration["AzureBlob:ContainerName"];

            var serviceClient = new BlobServiceClient(connectionString);
            _blobContainerClient = serviceClient.GetBlobContainerClient(containerName);
        }

        public async Task<string> UploadAsync(
            Stream fileStream,
            string fileName,
            string contentType
        )
        {
            var blobName = $"{Guid.NewGuid()}_{fileName}";
            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            var options = new BlobUploadOptions
            {
                HttpHeaders =  new BlobHttpHeaders
                {
                    ContentType =contentType
                }
            };

            await blobClient.UploadAsync(fileStream, options);

            // await blobClient.Uri.ToString();
            return blobName;
        }

        public async Task<(Stream Content, string ContentType)> DownloadAsync(string blobName)
        {
            var blobClient = _blobContainerClient.GetBlobClient(blobName);

            var response = await blobClient.DownloadAsync();
            return (response.Value.Content, response.Value.Details.ContentType);
        }
    }
}