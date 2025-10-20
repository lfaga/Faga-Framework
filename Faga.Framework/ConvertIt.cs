using System;
using System.Globalization;

namespace Faga.Framework
{
  public static class ConvertIt
  {
    public static bool ToBoolean(object o)
    {
      try
      {
        if (o is string)
        {
          var s = o.ToString().ToLowerInvariant();

          try
          {
            return int.Parse(s, CultureInfo.InvariantCulture) != 0;
          }
          catch (Exception)
          {
            string[] atrue = {"true", "on", "yes", "si", "verdadero"};
            string[] afalse = {"false", "off", "no", "falso"};

            if (Array.IndexOf(atrue, s) >= 0)
            {
              return true;
            }
            if (Array.IndexOf(afalse, s) >= 0)
            {
              return false;
            }
          }
        }
        return Convert.ToBoolean(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return ToBoolean(o, false);
      }
    }


    public static DateTime ToDateTime(object o)
    {
      try
      {
        return Convert.ToDateTime(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return ToDateTime(o, DateTime.MinValue);
      }
    }


    public static decimal ToDecimal(object o)
    {
      try
      {
        return Convert.ToDecimal(
          o, CultureInfo.InvariantCulture.NumberFormat);
      }
      catch
      {
        return ToDecimal(o, decimal.MinValue);
      }
    }


    public static int ToInt32(object o)
    {
      try
      {
        return Convert.ToInt32(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return ToInt32(o, int.MinValue);
      }
    }


    public static string ToString(object o)
    {
      return ToString(o, string.Empty);
    }


    public static bool ToBoolean(object o, bool defval)
    {
      try
      {
        return Convert.ToBoolean(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return defval;
      }
    }


    public static DateTime ToDateTime(object o, DateTime defval)
    {
      try
      {
        return Convert.ToDateTime(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return defval;
      }
    }


    public static decimal ToDecimal(object o, decimal defval)
    {
      try
      {
        return Convert.ToDecimal(
          o, CultureInfo.InvariantCulture.NumberFormat);
      }
      catch
      {
        return defval;
      }
    }


    public static int ToInt32(object o, int defval)
    {
      try
      {
        return Convert.ToInt32(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return defval;
      }
    }


    public static string ToString(object o, string defval)
    {
      try
      {
        return Convert.ToString(o, CultureInfo.InvariantCulture);
      }
      catch
      {
        return defval;
      }
    }
  }
}