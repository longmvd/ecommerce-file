using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Common.Utils
{
    public static class ValidateFileExtension
    {
        public static bool IsImageByExtension(this string filePath)
        {
            if(!string.IsNullOrWhiteSpace(filePath))
            {
                string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff", ".webp", ".ico", ".heif", ".heic" };

                string extension = Path.GetExtension(filePath)?.ToLowerInvariant();

                if (extension != null && Array.IndexOf(imageExtensions, extension) != -1)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
