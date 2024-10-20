using LEFiles.Core.Entities.Abstract;
using LEFiles.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class User : IEntity
  {
#pragma warning disable
    public User()
    {

    }
    public User(string firstname, string lastname, string username, string email, string password)
    {
      byte[] passwordSalt;
      byte[] passwordHash;
      HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
      UserId = Guid.NewGuid().ToString("N");
      Firstname = firstname;
      Lastname = lastname;
      Username = username;
      PasswordHash = passwordHash;
      PasswordSalt = passwordSalt;
      Email = email;
      RegistrationDate = DateTime.UtcNow;
    }

    public string UserId { get; set; }
    public string Email { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime RegistrationDate { get; set; }
    public virtual IList<Client> Clients { get; set; } = new List<Client>();
  }
}
