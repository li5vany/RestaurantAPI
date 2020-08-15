using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.API.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string msg, string entity) : base(msg)
        {
            Entity = entity;
        }

        public NotFoundException(string msg) : base(msg)
        {
        }

        public string Entity { get; set; }
    }
}