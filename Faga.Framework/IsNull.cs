using System;

namespace Faga.Framework
{
  public static class IsNull
  {
    public static bool This(object val)
    {
      if (val == null)
      {
        return true;
      }
      if (val is DateTime)
      {
        return This((DateTime) val);
      }
      if (val is decimal)
      {
        return This((decimal) val);
      }
      if (val is int)
      {
        return This((int) val);
      }
      if (val is string)
      {
        return This((string) val);
      }
      if (val is Guid)
      {
        return This((Guid) val);
      }
      if (val is DBNull)
      {
        return true;
      }

      return false;
    }


    public static bool This(Guid val)
    {
      return val.CompareTo(Guid.Empty) == 0;
    }


    public static bool This(DateTime val)
    {
      return val == DateTime.MinValue;
    }


    public static bool This(decimal val)
    {
      return val == decimal.MinValue;
    }


    public static bool This(int val)
    {
      return val == int.MinValue;
    }


    public static bool This(string val)
    {
      return string.IsNullOrEmpty(val);
    }
  }
}