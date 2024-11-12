using LEFiles.DataAccess;
using LEFiles.Services.Contracts.Clients;
using LEFiles.Services.ServiceModels.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Service.Clients
{
  public partial class ClientService : IClientService
  {
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
      _context = context;
    }
  }
}
