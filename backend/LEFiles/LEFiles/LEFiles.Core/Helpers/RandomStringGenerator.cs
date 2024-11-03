using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Core.Helpers
{
  public static class RandomStringGenerator
  {
    public static string Create(int len=15)
    {
      string allowed = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
      char[] randomChars = new char[len];

      for (int i = 0; i < len; i++)
      {
        randomChars[i] = allowed[RandomNumberGenerator.GetInt32(0, allowed.Length)];
      }

      string result = new string(randomChars);

      return result;
    }
  }
}
