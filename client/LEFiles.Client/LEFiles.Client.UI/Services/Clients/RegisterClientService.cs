using Flurl.Http;
using LEFiles.Client.Core.Helpers;
using LEFiles.Client.Core.Helpers.Singleton;
using LEFiles.Client.Core.XMLManager;
using LEFiles.Client.UI.Helpers.SystemInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.UI.Services.Clients
{
  public class RegisterClientService
  {
    private readonly SingleItemManager _singleItemManager;
    private readonly FlurlCoreClient _flurlClient;
    public RegisterClientService()
    {
      _singleItemManager = new SingleItemManager();
      _flurlClient = _singleItemManager.GetSingleItem<FlurlCoreClient>("flurl") ?? throw new Exception("Flurl Instance not found");
    }
    public bool Registered(){
      try{
        var xmlManager = new XMLManager();
        var readed = xmlManager.ReadXMLFile("client-configuration.xml", "Client");
        if(readed != null){
          return true;
        }
      }catch{
        return false;
      }
      return false;

    }
    public void Register(string token, string secret)
    {
      var winInfo = new WindowsSystemInformationHelper();
      var hddSn = winInfo.GetHddSerial();
      if(hddSn == null){
        throw new Exception();
      }
      var osVersion = Environment.OSVersion.Platform.ToString();
      var result = _flurlClient.RegisterClient(token, secret,hddSn.Trim(),osVersion);
      if(result != true){
        throw new Exception();
      }
      var xmlManager = new XMLManager();
      IDictionary<string,object> clientParams = new Dictionary<string,object>();
      clientParams.Add("secret", secret);
      clientParams.Add("hddSerial", hddSn);
      xmlManager.CreateXMLFile("client-configuration.xml", "Client", clientParams);
    }
  }
}
