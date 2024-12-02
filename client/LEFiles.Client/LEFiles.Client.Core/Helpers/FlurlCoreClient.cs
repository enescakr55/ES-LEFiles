using Flurl.Http;
using LEFiles.Client.Core.FlurlModels;
using LEFiles.Client.Core.Models.FlurlRequests;
using LEFiles.Client.Core.Models.FlurlResponses;
using LEFiles.Client.Core.ServiceResults.Abstract;
using LEFiles.Client.Core.ServiceResults.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Helpers
{
  public class FlurlCoreClient
  {
    private readonly string _apiUrl;
    private readonly string _harddiskSN;
    private readonly string _secret;
    public FlurlCoreClient(string apiUrl, string harddiskSN, string secret)
    {
      _apiUrl = apiUrl;
      _harddiskSN = harddiskSN;
      _secret = secret;
    }
    public static string? Token = null;
    public static DateTime? Expiration = null;
    public IFlurlRequest FlurlAnonymousRequest() {
      return _apiUrl.AllowAnyHttpStatus();
    }
    public IFlurlRequest GetFlurl() {
      GetToken();
      return _apiUrl.AllowAnyHttpStatus().WithHeader("Authorization", Token);
    }
    public bool RegisterClient(string token,string secret,string harddiskSn,string os){
      var request = new
      {
        Token = token,
        Secret = secret,
        HarddiskSN = harddiskSn,
        OperatingSystem = os
      };
      var serializedJson = JsonSerializer.Serialize(request);
      var reqResult = FlurlAnonymousRequest().AppendPathSegment("api/v1/clients/registration").PostJsonAsync(request).GetAwaiter().GetResult();
      if(reqResult.StatusCode != 200){
        throw new HttpRequestException("", null, (HttpStatusCode)reqResult.StatusCode);
      }
      var result = reqResult.GetJsonAsync<FlurlResult>().GetAwaiter().GetResult();
      if(result != null && result.Success){
        return true;
      }else{
        throw new HttpRequestException("", null, HttpStatusCode.InternalServerError);
      }
    }
    public void GetToken(){
      if(Expiration == null ||  DateTime.UtcNow.AddMinutes(2).Ticks > Expiration.Value.Ticks){
        var request = new ClientTokenRequest {
          HarddiskSN = _harddiskSN,
          Secret = _secret
        };
        var reqResult = (_apiUrl + "api/v1/clients/tokens/generate").AllowAnyHttpStatus().PostJsonAsync(request).GetAwaiter().GetResult();
        if(reqResult.StatusCode != 200){
          throw new HttpRequestException("", null, (HttpStatusCode)reqResult.StatusCode);
        }
        var result = reqResult.GetJsonAsync<FlurlDataResult<ClientTokenResponse>>().GetAwaiter().GetResult();
        if(result != null && result.Data != null && result.Success)
        {
          Token = result.Data.Token;
          Expiration = result.Data.Expiration;
        }else{
          throw new HttpRequestException("", null, HttpStatusCode.InternalServerError);
        }
      }
    }

  }
}
