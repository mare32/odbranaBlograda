using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Core
{
    public interface IFileUploader
    {
        string Upload(FileLocations location, IFormFile file);
    }
    public enum FileLocations
    {
        Images,
        Videos,
        Pdfs
    }
}
