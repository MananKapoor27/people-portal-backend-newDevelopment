using System;
using System.IO;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PeoplePortalServices.Shared.AWS
{
    public class AmazonS3Service : IAWSServices
    {
        private readonly String _accessKey;
        private readonly String _accessSecret;
        private readonly String _profilePictureBucket;
        private readonly IConfiguration _configuration;

        public AmazonS3Service(IConfiguration configuration)
        {
            _configuration = configuration;
            _accessKey = _configuration.GetSection("AmazonS3").GetSection("AccessKey").Value;
            _accessSecret = _configuration.GetSection("AmazonS3").GetSection("AccessSecret").Value;
            _profilePictureBucket = _configuration.GetSection("AmazonS3").GetSection("ProfilePictureBucket").Value;
        }

        public async Task<string> UploadObject(Guid employeeId, IFormFile file)
        {
            // connecting to the client
            var client = new AmazonS3Client(_accessKey, _accessSecret, Amazon.RegionEndpoint.APSouth1);

            // get the file and convert it to the byte[]
            byte[] fileBytes = new Byte[file.Length];
            file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

            // create unique file name for prevent the mess
            var fileName = employeeId.ToString();

            PutObjectResponse response = null;

            using (var stream = new MemoryStream(fileBytes))
            {
                var request = new PutObjectRequest
                {
                    BucketName = _profilePictureBucket,
                    Key = fileName,
                    InputStream = stream,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                response = await client.PutObjectAsync(request);
            };

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                // this model is up to you, in my case I have to use it following;
                return fileName;
            }
            else
            {
                // this model is up to you, in my case I have to use it following;
                //return new UploadPhotoModel
                //{
                //    Success = false,
                //    FileName = fileName
                //};
                return null;
            }
        }

        public async Task<string> RemoveObject(Guid employeeId)
        {
            var client = new AmazonS3Client(_accessKey, _accessSecret, Amazon.RegionEndpoint.APSouth1);

            var request = new DeleteObjectRequest
            {
                BucketName = _profilePictureBucket,
                Key = employeeId.ToString()
            };

            var response = await client.DeleteObjectAsync(request);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return employeeId.ToString();
            }
            else
            {
                //return new UploadPhotoModel
                //{
                //    Success = false,
                //    FileName = fileName
                //};
                return null;
            }
        }

    }
}
