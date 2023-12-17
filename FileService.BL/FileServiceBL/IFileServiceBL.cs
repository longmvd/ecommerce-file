using FileService.Common.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.BL.FileServiceBL
{
    public interface IFileServiceBL
    {
        Task<FileServiceResponse> DownLoadFile(string fileName, string fielPath = "Upload\\Files");

        Task<string> UploadFile(IFormFile file);

        Task<FileServiceResponse> GetFile(string fileName);

        Task<List<string>> UploadFiles(List<IFormFile> files);

        Task<bool> DeleteFile(string fileName);


    }
}
