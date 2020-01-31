using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    // Database Class
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // adds the properties that will be used to read and write
            // in here, it provides access to Product objects
        }
        // base : call the function/ feature in base class
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
        // class help to get set Database
    }
}
