using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.BasicResponses
{
    public class ApiCreatedResponse : ApiResponse
    {
        public ApiCreatedResponse(object result)
            : base(201)
        {
            Result = result;
        }

        public object Result { get; }
    }
}