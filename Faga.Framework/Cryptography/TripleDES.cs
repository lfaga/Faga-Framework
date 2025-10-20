using System;
using System.Security.Cryptography;
using System.Text;

namespace Faga.Framework.Cryptography
{
  public sealed class TripleDes
  {
    private TripleDes()
    {
    }


    private static TripleDESCryptoServiceProvider GetTripleDESProvider()
    {
      var des = new TripleDESCryptoServiceProvider();

      byte[] k = {5, 6, 3, 4, 5, 6, 2, 3, 9, 10, 11, 5, 13, 14, 3, 16, 17, 18, 19, 21, 20, 52, 23, 24};
      byte[] i = {8, 70, 6, 5, 41, 3, 20, 1};

      des.Key = k;
      des.IV = i;

      return des;
    }


    public static string Encrypt(string text)
    {
      using (var des = GetTripleDESProvider())
      {
        var Code = Encoding.ASCII.GetBytes(text);

        return Convert.ToBase64String(
          des.CreateEncryptor().TransformFinalBlock(Code, 0, Code.Length));
      }
    }


    public static string Decrypt(string text)
    {
      using (var des = GetTripleDESProvider())
      {
        var Code = Convert.FromBase64String(text);

        return Encoding.ASCII.GetString(
          des.CreateDecryptor().TransformFinalBlock(Code, 0, Code.Length));
      }
    }
  }
}