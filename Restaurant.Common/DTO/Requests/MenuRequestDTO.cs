using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Restaurant.Common.DTO.Requests
{
    public class MenuRequestDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        public decimal Price { get; set; }

        public int? MenuCardId { get; set; }
    }
}
