using Blog.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blog.Api.Core
{
    public class LocalFileUploader : IFileUploader
    {
        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpg", ".png", ".jpeg", ".gif" };

        public string Upload(FileLocations location, IFormFile file)
        {
            //var imgAlt = "";
            var guid = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Tip slike nije podrzan.");
            }
            
            //imgAlt = string.IsNullOrEmpty(dto.ImageAlt) ? "Slika nije ucitana" : dto.ImageAlt;

            var imeSlike = guid + extension;
            var putanja = Path.Combine("wwwroot", location.ToString().ToLower(), imeSlike);

            using var stream = new FileStream(putanja, FileMode.Create);
            file.CopyTo(stream);

            return imeSlike;
        }
    }
}
