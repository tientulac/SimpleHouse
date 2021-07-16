using SimpleHouse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SimpleHouse.Context
{
    public class DataContext: DbContext
    {
        public DataContext() : base("SimpleHouse")
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<SimpleHouse.Models.Brand> Brands { get; set; }
    }
}