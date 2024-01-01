using ECommerce.Common.DTO;
using FileService.BL.FileServiceBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace FileServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IFileServiceBL _fileService;
        public FilesController(IFileServiceBL fileService) 
        {
            _fileService = fileService;
        }
        
        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationtoken)
        {
            try
            {
                var result =await _fileService.UploadFile(file);
                var res = Ok(result);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("UploadFiles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, CancellationToken cancellationtoken)
        {
            try
            {
                var result = await _fileService.UploadFiles(files);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            try
            {
                var fileDownload = await _fileService.DownLoadFile(filename);
            
                return File(fileDownload.Bytes, fileDownload.ContentType, fileDownload.FileDownloadName);

            }catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFile([FromQuery]string filename)
        {
            try
            {
                var response = new ServiceResponse();
                var res = await _fileService.DeleteFile(filename);
                if (res)
                {
                    response.OnSuccess(res);
                }
                else
                {
                    response.OnError(new ErrorResponse());
                }
                return Ok(response);
                

            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteFiles([FromBody] List<string> fileNames)
        {
            try
            {
                var response = new ServiceResponse();
                var res = await _fileService.DeleteFiles(fileNames);
                if (res)
                {
                    response.OnSuccess(res);
                }
                else
                {
                    response.OnError(new ErrorResponse());
                }
                return Ok(response);


            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

    }
}
