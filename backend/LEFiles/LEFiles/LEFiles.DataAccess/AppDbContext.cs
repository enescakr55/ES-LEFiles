using Global.CoreProject.DataAccess;
using LEFiles.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace LEFiles.DataAccess
{
  public class AppDbContext : DbContext
  {
    DatabaseConnectModel ConnectModel { get; }
    public AppDbContext()
    {
      var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json", false, false).Build();
      ConnectModel = new DatabaseConnectModel();
      configuration.GetSection("Database").Bind(ConnectModel);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //base.OnConfiguring(optionsBuilder);
      base.OnConfiguring(optionsBuilder);
      switch (ConnectModel.Provider)
      {
        case "postgres":
          optionsBuilder.UseNpgsql(ConnectModel.ConnectionString, x => x.MigrationsAssembly("LEFiles.Data.Migrations.Postgres").MigrationsHistoryTable(HistoryRepository.DefaultTableName, ConnectModel.Schema ?? "lefiles"));
          break;
        case "mysql":
          optionsBuilder.UseMySql(ConnectModel.ConnectionString, ServerVersion.AutoDetect(ConnectModel.ConnectionString), (db) =>
          {
            db.MigrationsAssembly("LEFiles.Data.Migrations.Mysql");
          });
          break;
      }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
  }
}