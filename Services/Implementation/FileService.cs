using System.Reflection.Metadata;
using BlobStorageDemo.Repository;

namespace BlobStorageDemo.Service
{
    public class FileService : IFileService
    {
        private readonly IBlobStorageRepo _blobStorageRepo;

        public FileService(IBlobStorageRepo blobStorageRepo)
        {
            _blobStorageRepo = blobStorageRepo;
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if(file == null || file.Length == 0)
                throw  new ArgumentException("File is Empty");

            using var stream = file.OpenReadStream();

            return await _blobStorageRepo.UploadAsync(
                stream,
                file.FileName,
                file.ContentType
            );
        }

        public async Task<(byte[] Content, string ContentType)> DownloadFileAsync(string blobName)
        {
            var (stream, contentType) = await _blobStorageRepo.DownloadAsync(blobName);

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            return (ms.ToArray(), contentType);
        }

        public async Task DeleteFileAsync(string blobName)
        {
            if(string.IsNullOrWhiteSpace(blobName))
                throw new ArgumentException("Blob Name is Invalid");
            
            await _blobStorageRepo.DeleteAsync(blobName);
        }
    }
}