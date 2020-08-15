using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Data.Entities
{
    public class MenuCardEntity
    {
        public MenuCardEntity()
        {
            Menus = new List<MenuEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
        
        public DateTime CreateDate { get; set; }
        public DateTime LastModification { get; set; }

        public virtual List<MenuEntity> Menus { get; set; }
    }
}
