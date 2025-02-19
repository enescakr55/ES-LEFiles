using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global.CoreProject.Extensions
{
  public static class StringExtensions
  {
    public static string FirstCharToLowerCase(this string? str)
    {
      if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
        return str.Length == 1 ? char.ToLowerInvariant(str[0]).ToString() : char.ToLowerInvariant(str[0]) + str[1..];

      return str ?? "";
    }
    public static string OnlyFirstCharUpper(this string? str){
      if (!string.IsNullOrEmpty(str) && char.IsLower(str[0]))
        return str.Length == 1 ? char.ToUpperInvariant(str[0]).ToString() : char.ToUpperInvariant(str[0]) + str[1..].ToLowerInvariant();

      return str ?? "";
    }
  }
}
