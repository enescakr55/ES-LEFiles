using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Core.Models.Results.Concrete;
using LEFiles.DataAccess;
using LEFiles.Services.Contracts.Clients;
using LEFiles.Services.ServiceModels.Clients.Requests;

namespace LEFiles.API.Endpoints.ClientService.Registration
{
    public class RegisterClientEndpoint : BaseEndpoint<RegisterClientRequest, IResult>
  {
    private readonly AppDbContext _context;
    private readonly IClientService _clientService;
    public RegisterClientEndpoint(AppDbContext context, IClientService clientService)
    {
      _context = context;
      _clientService = clientService;
    }

    public override void Configure()
    {
      Post(ApiUrl + "clients/registration");
      AllowAnonymous();
    }
    public override async Task HandleAsync(RegisterClientRequest req, CancellationToken ct)
    {
      
      bool result = _clientService.RegisterClient(req);
      await SendAsync(new Result(result));
    }
  }
}
