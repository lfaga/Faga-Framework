using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace Faga.Framework.Collections
{
  public static class CollectionConvert
  {
    public static DataTable ListToTable(IList list)
    {
      DataTable dt = null;

      if (list != null)
      {
        dt = new DataTable {Locale = CultureInfo.InvariantCulture};

        if (list.Count > 0)
        {
          var enu = list.GetEnumerator();

          if (enu.MoveNext())
          {
            var t = enu.Current.GetType();

            CreateColumnsFromProperties(dt, t);

            do
            {
              dt.Rows.Add(FillRow(dt.NewRow(), enu.Current, t));
            } while (enu.MoveNext());
          }
        }
      }
      return dt;
    }


    public static DataTable CollectionToTable(ICollection collection)
    {
      if (collection != null)
      {
        var dt = new DataTable {Locale = CultureInfo.InvariantCulture};

        if (collection.Count > 0)
        {
          var enu = collection.GetEnumerator();

          if (enu.MoveNext())
          {
            var t = ((DictionaryEntry) enu.Current).Value.GetType();

            CreateColumnsFromProperties(dt, t);

            do
            {
              var o = ((DictionaryEntry) enu.Current).Value;
              dt.Rows.Add(FillRow(dt.NewRow(), o, t));
            } while (enu.MoveNext());
          }
        }
      }
      return null;
    }


    private static void CreateColumnsFromProperties(DataTable dt, Type classType)
    {
      foreach (var f in classType.GetProperties())
      {
        if (!MustIgnore(f))
        {
          var ptype = f.PropertyType;
          if (ptype.IsGenericType)
          {
            var gtd = ptype.GetGenericTypeDefinition();
            if (gtd.IsAssignableFrom(typeof (Nullable<>)))
            {
              ptype = new NullableConverter(ptype).UnderlyingType;
            }
          }
          dt.Columns.Add(f.Name, ptype);
        }
      }
    }


    private static DataRow FillRow(DataRow newRow, object o, Type oType)
    {
      foreach (var f in oType.GetProperties())
      {
        if (!MustIgnore(f))
        {
          var v = f.GetValue(o, null);
          if (v == null)
          {
            newRow[f.Name] = DBNull.Value;
          }
          else
          {
            newRow[f.Name] = v;
          }
        }
      }

      return newRow;
    }


    private static bool MustIgnore(PropertyInfo prop)
    {
      return prop.GetCustomAttributes(
        typeof (CollectionConvertIgnoreAttribute), true).Length > 0;
    }
  }
}