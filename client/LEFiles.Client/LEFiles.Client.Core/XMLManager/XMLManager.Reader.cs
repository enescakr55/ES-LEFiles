using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LEFiles.Client.Core.XMLManager
{
  public partial class XMLManager
  {
    public IDictionary<string, object> ReadXMLFile(string fileName, string elementName)
    {
      try
      {
        IDictionary<string,object> xmlData = new Dictionary<string,object>();
        var xmlString = File.ReadAllText(fileName);
        var stringReader = new StringReader(xmlString);
        var dataSet = new DataSet();
        dataSet.ReadXml(stringReader);
        var elementData = dataSet.Tables[elementName];
        if(elementData != null){
          var cols = elementData.Columns;
          var rows = elementData.Rows;
          
          for(var i=0;i<rows.Count; i++){
            for (int j = 0; j < cols.Count; j++)
            {
              xmlData.Add(cols[j].ColumnName, rows[i][cols[j].ColumnName]);
            }
          }
        }
        return xmlData;
        }
      catch
      {
        throw new Exception("Can't reading XML file");
      }
    }
  }
}
