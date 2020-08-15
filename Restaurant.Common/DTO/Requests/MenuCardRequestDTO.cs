using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Common.DTO.Requests
{
    public class MenuCardRequestDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
