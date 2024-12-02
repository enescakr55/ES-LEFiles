using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Helpers.Singleton
{
  public class SingleItemManager
  {
    private static IDictionary<string, object> items = new Dictionary<string,object>();
    public void SetSingleItem<T>(string key, T value){
      object? currentObj;
      if(items.TryGetValue(key, out currentObj)){
        items.Remove(key);
      }
      if(value != null){
        items.Add(key, value);
      }
    }
    public T? GetSingleItem<T>(string key){
      object? currentObj;
      if (items.TryGetValue(key, out currentObj))
      {
        return (T?)currentObj;
      }else{
        return default(T?);
      }
    }
  }
}
