using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BakeryMarketing.Models
{
  public class BakeryMarketingContextFactory : IDesignTimeDbContextFactory<BakeryMarketingContext>
  {

    BakeryMarketingContext IDesignTimeDbContextFactory<BakeryMarketingContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<BakeryMarketingContext>();

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"]));

      return new BakeryMarketingContext(builder.Options);
    }
  }
}