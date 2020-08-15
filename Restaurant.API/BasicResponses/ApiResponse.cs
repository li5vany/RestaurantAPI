using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.BasicResponses
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "Request with malformed syntax";

                case 401:
                    return "Unauthorized access. Requires user authentication";

                case 403:
                    return "Forbidden request";

                case 404:
                    return "Resource not found";

                case 405:
                    return "Operation not allowed for this resource";

                case 406:
                    return "Not acceptable response for this resource";

                case 408:
                    return "Request timeout";

                case 415:
                    return "Unsupported media type";

                case 500:
                    return "An unhandled error occurred";

                default:
                    return null;
            }
        }
    }
}