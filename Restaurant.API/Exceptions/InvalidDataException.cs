using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Exceptions
{
    public class InvalidDataException : BaseException
    {
        public InvalidDataException(string msg, string field) : base(msg)
        {
            Field = field;
        }

        public InvalidDataException(string msg) : base(msg)
        {
        }

        public string Field { get; set; }
    }
}