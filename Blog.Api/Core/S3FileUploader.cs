using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Blog.Api.Core
{
    public class S3FileUploader : IFileUploader
    {
        public string Upload(FileLocations location, IFormFile file)
        {
			using (var newMemoryStream = new MemoryStream())
			{
				var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
				file.CopyTo(newMemoryStream);

				var uploadRequest = new TransferUtilityUploadRequest
				{
					InputStream = newMemoryStream,
					Key = fileName,
					BucketName = "blograd-images",
					ContentType = file.ContentType
				};
				// ovi dole kljucevi treba iz json config fajla da se procitaju
				var s3Client = new AmazonS3Client("", "", RegionEndpoint.GetBySystemName("eu-central-1"));
				var fileTransferUtility = new TransferUtility(s3Client);

				fileTransferUtility.UploadAsync(uploadRequest).GetAwaiter().GetResult();

				return fileName;
			}
		}
    }
}
