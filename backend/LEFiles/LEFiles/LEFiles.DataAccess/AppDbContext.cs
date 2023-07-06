using LEFiles.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LEFiles.DataAccess
{
  public class AppDbContext : DbContext
  {
    public AppDbContext()
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //base.OnConfiguring(optionsBuilder);
      if (!optionsBuilder.IsConfigured)
      {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("Mssql");
        optionsBuilder.UseSqlServer(connectionString);
      }

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
  }
}