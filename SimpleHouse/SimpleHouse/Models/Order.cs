using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SimpleHouse.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Telephone { get; set; }
        public String Address { get; set; }
        public decimal GrandTotal { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
    }
}