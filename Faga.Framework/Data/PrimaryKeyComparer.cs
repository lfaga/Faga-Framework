using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Faga.Framework.Data
{
  public class PrimaryKeyComparer<TElement>
    : Comparer<TElement>
  {
    /// <summary>
    ///   When overridden in a derived class, performs a comparison of two objects of the same type and returns a value
    ///   indicating whether one object is less than, equal to, or greater than the other.
    /// </summary>
    /// <returns>
    ///   Value Condition Less than zero x is less than y.Zero x equals y.Greater than zero x is greater than y.
    /// </returns>
    /// <param name="y">The second object to compare.</param>
    /// <param name="x">The first object to compare.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Type T does not implement either the
    ///   <see cref="T:System.IComparable`1"></see> generic interface or the <see cref="T:System.IComparable"></see> interface.
    /// </exception>
    public override int Compare(TElement x, TElement y)
    {
      var t = typeof (TElement);
      var primaryKeys = new Collection<PropertyInfo>();

      foreach (var property in t.GetProperties())
      {
        var attrs = property.GetCustomAttributes(
          typeof (PrimaryKeyAttribute), true);
        if (attrs.Length > 0)
        {
          primaryKeys.Add(property);
        }
      }

      if (primaryKeys.Count > 0)
      {
        foreach (var primaryKey in primaryKeys)
        {
          var valx = primaryKey.GetValue(x, null) as IComparable;
          var valy = primaryKey.GetValue(y, null) as IComparable;

          if ((valx == null) || (valy == null)
              || (valx.CompareTo(valy) != 0))
          {
            return -1;
          }
        }
        return 0;
      }
      throw new InvalidOperationException(
        string.Format("{0} has no PrimaryKey properties.", t.Name));
    }
  }
}