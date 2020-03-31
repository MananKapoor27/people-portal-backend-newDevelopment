using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PeoplePortalServices.Shared.AWS
{
    /// <summary>
    /// this class is used to access the AWS credentials from web.config
    /// </summary>
    public class AWSConfig
    {
        private readonly IConfiguration _configuration;

        public  AWSConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// this is used to get the Access Key of the S3 bucket 
        /// </summary>
        public string AccessKey => _configuration["AWSAccessKey"];
        /// <summary>
        /// this is used to get the Security Key of the S3 bucket
        /// </summary>
        public string SecretKey => _configuration["AWSSecretKey"];
        /// <summary>
        /// this is used to get the S3 bucket name
        /// </summary>
        public string ProfilePhotoFolder => _configuration["ProfilePhotos"];
        /// <summary>
        /// this is used to get the default picture key
        /// </summary>
        public string DefaultProfilePhoto => _configuration["DefaultImage"];
    }
}
