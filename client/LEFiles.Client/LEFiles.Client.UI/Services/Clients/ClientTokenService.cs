using LEFiles.Client.Core.Helpers.Singleton;
using LEFiles.Client.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.UI.Services.Clients
{
  public class ClientTokenService
  {
    private readonly SingleItemManager _singleItemManager;
    private readonly FlurlCoreClient _flurlClient;
    public ClientTokenService()
    {
      _singleItemManager = new SingleItemManager();
      _flurlClient = _singleItemManager.GetSingleItem<FlurlCoreClient>("flurl") ?? throw new Exception("Flurl Instance not found");
    }
    public bool GenerateToken()
    {
      try
      {
        _flurlClient.GetToken();
        return true;
      }
      catch
      {
        return false;
      }


    }
  }
}
