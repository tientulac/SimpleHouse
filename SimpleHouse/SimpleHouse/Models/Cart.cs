using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleHouse.Models
{
    public class Cart
    {
        private Customer customer;
        private List<CartItem> cartItems;
        private decimal grandTotal;

        public Cart()
        {
            cartItems = new List<CartItem>();
        }

        public List<CartItem> CartItems { get => cartItems; }
        public decimal GrandTotal { get => grandTotal; set => grandTotal = value; }
        public Customer Customer { get => customer; set => customer = value; }

        public CartItem this[int index]  // indexer
        {
            get => CartItems[index];
            set => CartItems[index] = value;
        }

        public bool AddToCart(CartItem item)
        {
            // kiem tra xem co sp chua
            int check = CheckExists(item);
            if (check >= 0)
            {
                CartItems[check].Quantity += item.Quantity;
            }
            else
            {
                CartItems.Add(item);
            }
            CalculateGrandTotal();
            return true;
        }

        public void RemoveItem(int id)
        {
            foreach (var item in CartItems)
            {
                if (item.Product.Id == id)
                {
                    CartItems.Remove(item);
                    CalculateGrandTotal();
                    return;
                }
            }
        }

        public int CheckExists(CartItem item)
        {
            for (int i = 0; i < CartItems.Count; i++)
            {
                if (CartItems[i].Product.Id == item.Product.Id)
                {
                    return i;
                }

            }
            return -1;
        }

        public void CalculateGrandTotal()
        {
            decimal grand = 0;
            foreach (CartItem item in CartItems)
            {
                grand += item.Product.Price * item.Quantity;
            }
            GrandTotal = grand;
        }
    }
}