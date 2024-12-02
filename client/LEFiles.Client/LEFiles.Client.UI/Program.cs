using Flurl.Http;
using LEFiles.Client.Core.Helpers;
using LEFiles.Client.Core.Helpers.Singleton;
using LEFiles.Client.Core.Serializers;
using LEFiles.Client.Core.XMLManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Text.Json;

namespace LEFiles.Client.UI
{
  internal static class Program
  {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      ApplicationConfiguration.Initialize();

      FlurlHttp.Configure(settings =>
      {
        var jsonSettings = new JsonSerializerOptions
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          WriteIndented = true,
          DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
          PropertyNameCaseInsensitive = true,
        };
        var serializer = new FlurlSerializer(jsonSettings);
        settings.JsonSerializer = serializer;
      });
      XMLManager xmlManager = new XMLManager();
      var apiurl = ConfigurationManager.AppSettings["apiUrl"];
      if(apiurl == null){
        throw new Exception("Api Url not defined");
      }
      FlurlCoreClient _flurlClient = new FlurlCoreClient(apiurl,"","");

      var singleItemManager = new SingleItemManager();
      singleItemManager.SetSingleItem<FlurlCoreClient>("flurl", _flurlClient);
      
      Application.Run(new UserInterface());
    }
  }
}