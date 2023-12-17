using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Common.DTO
{
    public class FileServiceResponse
    {
        public byte[]? Bytes { get; set; }

        public string? ContentType { get; set; }

        public string? FileDownloadName { get; set; }

    }
}
