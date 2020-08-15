using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Common.DTO.Responses
{
    public class MenuResponseDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Type { get; set; }

        public decimal Price { get; set; }

        public bool Active { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime LastModification { get; set; }

        public int MenuCardId { get; set; }
    }
}
