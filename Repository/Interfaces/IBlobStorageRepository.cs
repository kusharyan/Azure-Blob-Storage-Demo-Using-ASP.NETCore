namespace BlobStorageDemo.Repository
{
    public interface IBlobStorageRepo
    {
        Task<string> UploadAsync(
            Stream fileStream,
            string fileName,
            string contentType
        );

        Task<(Stream Content, string ContentType)> DownloadAsync(string blobName);
    }
}

