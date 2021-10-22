using Microsoft.EntityFrameworkCore;

namespace BakeryMarketing.Models
{
  public class BakeryMarketingContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<>  { get; set; }
    public DbSet<>  { get; set; }
    public DbSet<>  { get; set; }

    public BakeryMarketingContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}