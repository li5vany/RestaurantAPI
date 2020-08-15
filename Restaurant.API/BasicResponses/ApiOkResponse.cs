using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.BasicResponses
{
    public class ApiOkResponse : ApiResponse

    {
        public ApiOkResponse(object result)
            : base(200)
        {
            Result = result;
        }

        public object Result { get; }
    }
}