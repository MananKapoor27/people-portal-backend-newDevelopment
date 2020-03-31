using Amazon.S3;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeoplePortalServices.Shared.AWS
{
    /// <summary>
    /// this interface is used to access the AWS account of the user
    /// </summary>
    public interface IAWSServices
    {
        /// <summary>
        /// this is used to get the access the AWS account of the user
        /// </summary>
        //IAmazonS3 Client { get; }

        Task<string> UploadObject(Guid employeeId, IFormFile file);

        Task<string> RemoveObject(Guid employeeId);

    }
}
