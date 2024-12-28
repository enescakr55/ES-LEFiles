using Global.CoreProject.DataAccess;
using LEFiles.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;

namespace LEFiles.DataAccess
{
  public partial class AppDbContext : DbContext
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
    if(ConnectModel.Provider == "postgres"){
        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.HasDefaultSchema(ConnectModel.Schema ?? "lefiles");
      }
      base.OnModelCreating(modelBuilder);
      this.BuildModels(modelBuilder);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientRegistrationToken> ClientRegistrationTokens { get; set; }
    public DbSet<ClientSession> ClientSessions { get; set; }
    public DbSet<FileItem> FileItems { get; set; }
    public DbSet<FolderItem> FolderItems { get; set; }
    public DbSet<WaitableRequest> WaitableRequests { get; set; }
    public DbSet<FileUploadItem> FileUploadItems { get; set; }
  }
}