using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.BasicResponses
{
    public class ApiServiceUnavailableResponse : ApiResponse
    {
        public ApiServiceUnavailableResponse(object result)
           : base(500)
        {
            Result = result;
        }

        public object Result { get; }
    }
}