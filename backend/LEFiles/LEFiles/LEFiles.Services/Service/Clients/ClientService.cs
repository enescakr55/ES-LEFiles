using LEFiles.DataAccess;
using LEFiles.Models.Configuration;
using LEFiles.Services.Contracts.Authentication;
using LEFiles.Services.Contracts.Clients;
using LEFiles.Services.ServiceModels.Clients;
using LEFiles.Services.ServiceModels.Clients.Requests;
using LEFiles.Services.ServiceModels.Clients.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Services.Service.Clients
{
  public partial class ClientService : IClientService
  {
    private readonly List<JWTConfig> _jwtConfig;
    private readonly AppDbContext _context;
    private readonly IAuthenticationService _authenticationService;

    public ClientService(AppDbContext context, List<JWTConfig> jwtConfig, IAuthenticationService authenticationService)
    {
      _context = context;
      _jwtConfig = jwtConfig;
      _authenticationService = authenticationService;
    }


  }
}
