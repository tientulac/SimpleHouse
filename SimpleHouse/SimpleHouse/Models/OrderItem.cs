using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SimpleHouse.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
    }
}