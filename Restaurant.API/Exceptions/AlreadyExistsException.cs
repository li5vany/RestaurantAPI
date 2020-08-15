using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException(string msg) : base(msg)
        {
        }
    }
}