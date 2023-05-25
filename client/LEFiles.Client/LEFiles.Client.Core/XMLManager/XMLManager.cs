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
    XmlWriterSettings writerSettings;
    XmlWriter writer;
    public XMLManager()
    {
      writerSettings = new XmlWriterSettings();
      writerSettings.Indent = true;
      writer = XmlWriter.Create("UserSettings.xml", writerSettings);
      writer.WriteStartDocument();
      writer.WriteStartElement("Key");
      writer.WriteValue(Guid.NewGuid().ToString("N"));
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Close();
      writer.Flush();

      Console.WriteLine("XML document created");
    }
     
  }
}
