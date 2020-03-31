using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PeoplePortalDomainLayer.Entities.DTO.ErrorstatusDto;
using PeoplePortalDomainLayer.Entities.DTO.SuccessStatusDTO;

namespace PeoplePortalServices.Shared.Helpers
{
    public static class CreateResponseService
    {
        /// <summary>
        /// this method is used to create a response
        /// </summary>
        /// <param name="code">status code</param>
        /// <param name="data">response message with its status(SuccessStatus/ErrorStatus Class objects)</param>
        /// <returns>it will return a  response with particular message and status code </returns>
        public static HttpResponseMessage CreateResponse(HttpStatusCode code, object data)
        {
            //manually converting object data into camelCase Json Data
            var jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return new HttpResponseMessage
            {
                StatusCode = code,
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

        }

        /// <summary>
        /// this method is used to create a error resoponse with errorStatus class object
        /// </summary>
        /// <param name="code">status code</param>
        /// <param name="message">error message for response</param>
        /// <param name="reason">Reason of error</param>
        /// <returns>it will return a error response with particular statusCode </returns>
        public static HttpResponseMessage CreateErrorResponse(HttpStatusCode code, string reason, string message)
        {
            var errorStatus = new ErrorStatusDto(Convert.ToInt32(code).ToString(), message);
            var result = CreateResponse(code, errorStatus);
            result.ReasonPhrase = reason;
            result.Headers.Add("X-Error", "error message");
            return result;
        }

        /// <summary>
        /// this method is used to create a success resoponse with successStatus class object for early return situations
        /// </summary>
        /// <param name="code">status code</param>
        /// <param name="data">data for response</param>     
        /// <returns>it will return a success response with particular statusCode</returns>
        public static HttpResponseMessage CreateSuccessResponse(HttpStatusCode code, object data)
        {
            var successStatus = new SuccessStatusDto { Data = data };
            return CreateResponse(code, successStatus);
        }

        /// <summary>
        /// this method is used to create a success resoponse with successStatus class object for early return situations
        /// </summary>
        /// <param name="code">status code</param>
        /// <param name="data">data for response</param>    
        /// <param name="records">records for pagination</param>
        /// <returns>it will return a success response with particular statusCode</returns>
        public static HttpResponseMessage CreateCompleteSuccessResponse(HttpStatusCode code, object data, RecordsDto records)
        {
            var successStatus = new SuccessStatusDto() { Data = data, Record = records };

            return CreateResponse(code, successStatus);
        }
    }
}