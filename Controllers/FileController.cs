using BlobStorageDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorageDemo.Controller
{
    [ApiController]
    [Route("api/files")]
    public class BlobController : ControllerBase
    {
        private readonly IFileService _fileService;

        public BlobController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var blobName = await _fileService.UploadFileAsync(file);
            
            return Ok(
                new
                {
                    BlobName = blobName,
                    Message = "File Uploaded Succesfully!"
                }
            );
        }

        [HttpGet("download/{blobName}")]
        public async Task<IActionResult> DownloadFile(string blobName)
        {
            var (content, contentType) = await _fileService.DownloadFileAsync(blobName);

            return File(
                content,
                contentType,
                fileDownloadName: blobName
            );
        }
    }
}