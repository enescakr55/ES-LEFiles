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
    public void CreateXMLFile(string fileName,string elementName, IDictionary<string, object> values)
    {
      var writerSettings = new XmlWriterSettings();
      writerSettings.Indent = true;
      writerSettings.Encoding = Encoding.UTF8;
      var writer = XmlWriter.Create(fileName, writerSettings);
      writer.WriteStartDocument();
      var keys = values.Keys.ToList();
      writer.WriteStartElement(elementName);
      keys.ForEach(key =>
      {
        var valAvailabe = values.TryGetValue(key, out var val);
        if (valAvailabe && val != null)
        {

          writer.WriteElementString(key,val.ToString());

        }
      });
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Close();
      writer.Flush();
    }
  }
}
