using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleHouse.Models
{
    public class CartItem
    {
        public CartItem(Product product, int qty)
        {
            Product = product;
            Quantity = qty;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}