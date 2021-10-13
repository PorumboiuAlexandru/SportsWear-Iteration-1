using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportsWear2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsWear2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Stock> Stocks { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<Order> Orders { set; get; }
    }
}
