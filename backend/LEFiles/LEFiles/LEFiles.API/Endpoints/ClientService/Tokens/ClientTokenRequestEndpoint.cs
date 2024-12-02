using LEFiles.Core.Endpoints;
using LEFiles.Core.Models.Results.Abstract;
using LEFiles.Services.Contracts.Clients;
using LEFiles.Services.ServiceModels.Clients.Requests;
using LEFiles.Services.ServiceModels.Clients.Responses;

namespace LEFiles.API.Endpoints.ClientService.Tokens
{
    public class ClientTokenRequestEndpoint : BaseEndpoint<GetClientTokenRequest, IDataResult<ClientTokenResponse>>
    {
        private readonly IClientService _clientService;

        public ClientTokenRequestEndpoint(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override void Configure()
        {
            Post(ApiUrl + "clients/tokens/generate");
            AllowAnonymous();
        }
        public override async Task HandleAsync(GetClientTokenRequest req, CancellationToken ct)
        {
            await SendAsync(_clientService.GetClientToken(req));
        }
    }
}
