namespace BlobStorageDemo.Service
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task<(byte[] Content, string ContentType)> DownloadFileAsync(string blobName);
        Task DeleteFileAsync(string blobName);
    }
}