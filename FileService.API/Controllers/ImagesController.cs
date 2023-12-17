using FileService.BL.FileServiceBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IFileServiceBL _fileService;
        
        public ImagesController(IFileServiceBL fileServiceBL)
        {
            _fileService = fileServiceBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string filename)
        {
            try
            {
                var fileDownload = await _fileService.GetFile(filename);

                return File(fileDownload.Bytes, fileDownload.ContentType);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return BadRequest();
            }

        }
    }
}
