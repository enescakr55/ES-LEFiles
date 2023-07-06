using LEFiles.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Models.Entities
{
  public class Client:IEntity
  {
    public Client()
    {

    }
    public Client(string clientId, string clientName, string clientSecret, string operatingSystem, string harddiskSerialNumber, bool isActive, DateTime? endDate)
    {
      ClientId = clientId;
      ClientName = clientName;
      ClientSecret = clientSecret;
      OperatingSystem = operatingSystem;
      HarddiskSerialNumber = harddiskSerialNumber;
      IsActive = isActive;
      EndDate = endDate;
    }
    public Client(string clientName, string clientSecret, string operatingSystem, string harddiskSerialNumber, bool isActive, DateTime? endDate)
    {
      ClientId = Guid.NewGuid().ToString("N");
      ClientName = clientName;
      ClientSecret = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
      OperatingSystem = operatingSystem;
      HarddiskSerialNumber = harddiskSerialNumber;
      IsActive = isActive;
      EndDate = endDate;
    }
    [Key]
    public string ClientId { get; set; }
    public string ClientName { get; set; }
    public string ClientSecret { get; set; }
    public string OperatingSystem { get; set; }
    public string HarddiskSerialNumber { get; set; }
    public bool IsActive { get; set; }
    public DateTime? EndDate { get; set; }
  }
}
