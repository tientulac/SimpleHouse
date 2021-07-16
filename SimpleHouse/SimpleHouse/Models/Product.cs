using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleHouse.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; } // khoa ngoai
        public int BrandID { get; set; }

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
    }
}