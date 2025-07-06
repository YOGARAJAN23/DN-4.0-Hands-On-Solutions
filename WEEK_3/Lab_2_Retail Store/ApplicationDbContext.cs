using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetailInventory.Models;

namespace RetailInventory.Data
{
    public class AppDbContext:DbContext

    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=RetailInventoryDB2;Trusted_Connection=True;");
        }
    }
}