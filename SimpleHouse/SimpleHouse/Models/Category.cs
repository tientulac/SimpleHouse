using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SimpleHouse.Models
{
    public class Category
    {
        [Key] // khoa chinh
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên danh mục")]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; } // giong hasMany
    }
}