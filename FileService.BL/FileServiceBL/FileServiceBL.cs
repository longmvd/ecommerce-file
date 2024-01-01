using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileService.Common.DTO;
using Microsoft.AspNetCore.Http;
using FileService.Common.Utils;

namespace FileService.BL.FileServiceBL
{
    public class FileServiceBL : IFileServiceBL
    {
        public async Task<bool> DeleteFile(string fileName)
        {
            bool result = false;
            if (fileName.IsImageByExtension())
            {
                result = await DeleteFileByName(fileName, "Upload/Images");

            }
            else
            {
                result = await DeleteFileByName(fileName);
            }
            return result;
        }

        public async Task<FileServiceResponse>  DownLoadFile(string fileName, string filePath = "Upload/Files")
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }

            var bytes = await File.ReadAllBytesAsync(filepath);

            return new FileServiceResponse() { Bytes = bytes, ContentType = contenttype, FileDownloadName = Path.GetFileName(filepath)};
        }

        public async Task<FileServiceResponse> GetFile(string fileName)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload/Images", fileName);

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "image/jpeg";
            }

            var bytes = await File.ReadAllBytesAsync(filepath);

            return new FileServiceResponse() { Bytes = bytes, ContentType = contenttype};
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            var filename = DateTime.Now.Ticks.ToString() + extension;
            var result = "";
            if(extension.IsImageByExtension()) 
            {
                result = await WriteFile(file, "Upload/Images");

            }
            else
            {
                result = await WriteFile(file);
            }

            return result;
        }

        public async Task<List<string>> UploadFiles(List<IFormFile> files)
        {
            var savedFileNames = new List<string>();
            foreach(var file in files)
            {
                var fileName = await UploadFile(file);
                savedFileNames.Add(fileName);

            }

            return savedFileNames;
        }

        private async Task<string> WriteFile(IFormFile file, string filePath = "Upload/Files")
        {
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), filePath, filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return filename;

            }
            catch (Exception ex)
            {

            }
            return filename;
        }


        private async Task<bool> DeleteFileByName(string filename, string filePath = "Upload/Files")
        {
            
            try
            {
                //var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                //filename = DateTime.Now.Ticks.ToString() + extension;
                filePath = Path.Combine(Directory.GetCurrentDirectory(), filePath, filename);

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Delete the file
                    await Task.Run(() => File.Delete(filePath));
                    return true;
                }
                else
                {

                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            
        }

        public  async Task<bool> DeleteFiles(List<string> fileNames)
        {
            try
            {
                var tasks = new List<Task<bool>>();
                foreach(var fileName in fileNames)
                {
                    tasks.Add(DeleteFile(fileName));
                }
                var results = await Task.WhenAll(tasks);
                return results.Any(res => res);

            }catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
