using System;
using System.Text;

namespace Faga.Framework.Utility
{
  /// <summary>
  ///   Summary description for StringUtility.
  /// </summary>
  public class StringUtility
  {
    private readonly string _work;


    public StringUtility(string input)
    {
      _work = input;
    }


    public string ReplaceIgnoreCase(string find, string replace)
    {
      if (find == null)
      {
        throw new ArgumentNullException("find");
      }

      if (replace == null)
      {
        throw new ArgumentNullException("replace");
      }

      var result = new StringBuilder(_work);
      var upperWork = _work.ToUpperInvariant();
      find = find.ToUpperInvariant();

      for (var startIndex = upperWork.IndexOf(find, StringComparison.Ordinal);
        startIndex != -1;
        startIndex = upperWork.IndexOf(find, StringComparison.Ordinal))
      {
        result.Remove(startIndex, find.Length);
        if (replace.Length > 0)
        {
          result.Insert(startIndex, replace);
        }
      }

      return result.ToString();
    }
  }
}