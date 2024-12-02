using Flurl.Http.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LEFiles.Client.Core.Serializers
{
  public class FlurlSerializer : ISerializer
  {

      private readonly JsonSerializerOptions? _options;

      public FlurlSerializer(JsonSerializerOptions? options = null)
      {
        _options = options;
      }

      public T Deserialize<T>(string s)
      {
        return JsonSerializer.Deserialize<T>(s, _options)!;
      }

      public T Deserialize<T>(Stream stream)
      {
        using var reader = new StreamReader(stream);
        return Deserialize<T>(reader.ReadToEnd());
      }

      public string Serialize(object obj)
      {
        return JsonSerializer.Serialize(obj, _options);
      }
  }
}
