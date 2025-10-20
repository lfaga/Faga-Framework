using System;
using System.Collections.ObjectModel;

namespace Faga.Framework.Security.Model.Collections
{
  [Serializable]
  public class PermissionValueCollection : Collection<PermissionValue>
  {
    public new bool Contains(PermissionValue item)
    {
      foreach (var val in this)
      {
        if ((val.IdApplication == item.IdApplication)
            && (val.IdPermission == item.IdPermission)
            && (val.Key.CompareTo(item.Key) == 0)
            && (val.Value.CompareTo(item.Value) == 0))
        {
          return true;
        }
      }

      return false;
    }
  }
}