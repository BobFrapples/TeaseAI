using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeaseAI.Common.Data;

namespace TeaseAI.Common.Interfaces
{
    public interface IImageBlogDownloadService
    {
        Task<Result<List<ImageMetaData>>> GetBlogImagesAsync(Uri baseUrl, int offset, int count);
    }
}
