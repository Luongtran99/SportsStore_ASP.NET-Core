using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SportsStore.Models
{
    public class DesignTimeDbContextFactory2 : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppIdentityDbContext>();

            var connectionString = configuration["Data:SportStoreProducts:ConnectionString"];

            builder.UseSqlServer(connectionString);

            return new AppIdentityDbContext(builder.Options);
        }
    }
}
