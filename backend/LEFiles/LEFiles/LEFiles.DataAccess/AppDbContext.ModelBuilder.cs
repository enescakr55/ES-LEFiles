using LEFiles.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.DataAccess
{
  public partial class AppDbContext
  {
    public ModelBuilder BuildModels(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Client>(x =>
      {
        x.HasKey(p => p.ClientId);
        x.Property(p => p.ClientId).HasMaxLength(50).IsRequired();
        x.Property(p => p.ClientName).HasMaxLength(100).IsRequired();
        x.Property(p => p.ClientSecret).HasMaxLength(100).IsRequired();
        x.Property(p => p.Description).HasMaxLength(500);
        x.Property(p => p.OperatingSystem).HasMaxLength(100);
        x.Property(p => p.HarddiskSerialNumber).HasMaxLength(100);
        x.Property(p => p.UserId).HasMaxLength(50).IsRequired();
        x.HasIndex(p => p.ClientId).IsUnique();

        x.HasOne(x => x.User).WithMany(x => x.Clients).HasForeignKey(x => x.UserId);
      });
      modelBuilder.Entity<ClientRegistrationToken>(x =>
      {
        x.HasKey(p => p.Id);
        x.HasIndex(p => p.Id).IsUnique();
        x.Property(x => x.ClientName).HasMaxLength(50).IsRequired();
        x.Property(p => p.Id).UseIdentityAlwaysColumn().IsRequired();
        x.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        x.Property(x => x.Secret).HasMaxLength(100).IsRequired();
        x.Property(x => x.Token).HasMaxLength(500).IsRequired();

        x.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
      });
      modelBuilder.Entity<ClientSession>(entity =>
      {
        entity.HasKey(x => x.ClientSessionId);
        entity.HasIndex(x => x.ClientSessionId).IsUnique();
        entity.Property(x => x.SessionCode).HasMaxLength(50).IsRequired();
        entity.Property(x => x.ClientId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.ClientSessionId).HasMaxLength(50).IsRequired();

        entity.HasOne(x => x.Client).WithMany(x => x.ClientSessions).HasForeignKey(x => x.ClientId);
      });
      modelBuilder.Entity<FileUploadItem>(entity =>
      {
        entity.HasKey(x => x.Id);

        entity.Property(x => x.Id).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Extension).HasMaxLength(5);
        entity.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.FileName).HasMaxLength(1000);
        entity.Property(x => x.FilePath).HasMaxLength(1000);
        entity.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        
      });
      modelBuilder.Entity<FileItem>(entity =>
      {
        entity.HasKey(x => x.FileId);
        entity.HasIndex(x => x.FileId).IsUnique();
        entity.Property(x => x.FileId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Extension).HasMaxLength(20).IsRequired();
        entity.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
        entity.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.ParentFolderId).HasMaxLength(50);
        entity.Property(x => x.FileName).HasMaxLength(100).IsRequired();
        entity.Property(x => x.FilePath).HasMaxLength(100).IsRequired();
        entity.Property(x => x.FileUploadId).HasMaxLength(50);
        entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        entity.HasOne(x => x.ParentFolder).WithMany(x => x.Files).HasForeignKey(x => x.ParentFolderId).OnDelete(DeleteBehavior.Cascade);
        entity.HasOne(x => x.FileUploadItem).WithOne().HasForeignKey<FileItem>(x => x.FileUploadId);

      });
      modelBuilder.Entity<FolderItem>(entity =>
      {
        entity.HasKey(x => x.FolderId);
        entity.HasIndex(x => x.FolderId).IsUnique();
        entity.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.ParentFolderId).HasMaxLength(50);
        entity.Property(x => x.FolderId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.FolderName).HasMaxLength(50).IsRequired();

        entity.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        entity.HasOne(x => x.ParentFolder).WithMany(x => x.Childs).HasForeignKey(a => a.ParentFolderId).OnDelete(DeleteBehavior.Cascade);
      });
      modelBuilder.Entity<User>(entity =>
      {
        entity.HasKey(x => x.UserId);
        entity.HasIndex(x => x.UserId).IsUnique();
        entity.HasIndex(x => x.Username).IsUnique();
        entity.HasIndex(x => x.Email).IsUnique();
        entity.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Email).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Username).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Firstname).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Lastname).HasMaxLength(50).IsRequired();
      });
      modelBuilder.Entity<WaitableRequest>(entity =>
      {
        entity.HasKey(x => x.RequestId);
        entity.HasIndex(x => x.RequestId).IsUnique();
        entity.Property(x => x.UserId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.ClientId).HasMaxLength(50).IsRequired();
        entity.Property(x => x.RequestContent).HasMaxLength(10000).IsRequired();
        entity.Property(x => x.Response).HasMaxLength(10000).IsRequired();

        entity.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        entity.HasOne<Client>().WithMany().HasForeignKey(x => x.ClientId);

      });
      return modelBuilder;
    }
  }
}
