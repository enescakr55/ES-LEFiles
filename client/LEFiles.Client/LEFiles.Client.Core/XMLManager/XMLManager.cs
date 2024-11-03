using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LEFiles.Client.Core.XMLManager
{
  public partial class XMLManager
  {
    private readonly string Test = "";
    public XMLManager()
    {
      IDictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary.Add("allowFileUpload", true);
      dictionary.Add("allowSystemFiles", false);
      dictionary.Add("allowHiddenFiles", false);
      this.CreateXMLFile("lefiles.settings.xml","UserSettings", dictionary);
      ReadXMLFile("lefiles.settings.xml", "UserSettings");
    }

  }
}
