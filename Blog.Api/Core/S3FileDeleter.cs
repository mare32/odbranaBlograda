using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace Blog.Api.Core
{
    public class S3FileDeleter : IFileDeleter
    {
        public void DeleteFile(string fileName)
        {
            DeleteObjectRequest request = new DeleteObjectRequest
            {
                BucketName = "blograd-images",
                Key = fileName
            };

            //if (!string.IsNullOrEmpty(versionId))
            //    request.VersionId = versionId;

            var s3Client = new AmazonS3Client("", "", RegionEndpoint.GetBySystemName("eu-central-1"));

            s3Client.DeleteObjectAsync(request).GetAwaiter().GetResult();
        }
    }
}
