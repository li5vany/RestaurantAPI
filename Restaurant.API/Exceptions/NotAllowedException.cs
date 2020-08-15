using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Exceptions
{
    public class NotAllowedException : BaseException
    {
        public NotAllowedException(string msg) : base(msg)
        {
        }
    }
}
