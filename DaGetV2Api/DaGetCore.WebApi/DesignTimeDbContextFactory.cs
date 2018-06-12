using DaGetCore.Dal.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DaGetCore.WebApi
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DaGetContext>
    {
        public DaGetContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<DaGetContext>();
            var connectionString = configuration.GetConnectionString("DaGetConnexionString");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("DaGetCore.WebApi"));
            return new DaGetContext(builder.Options);
        }
    }
}
