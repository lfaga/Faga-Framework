using System;
using System.Collections.ObjectModel;

namespace Faga.Framework.Security.Model.Collections
{
  [Serializable]
  public class PermissionParamCollection : Collection<PermissionParam>
  {
    public new bool Contains(PermissionParam item)
    {
      foreach (var param in this)
      {
        if ((param.IdApplication == item.IdApplication)
            && (param.IdPermission == item.IdPermission)
            && (param.Key.CompareTo(item.Key) == 0))
        {
          return true;
        }
      }

      return false;
    }
  }
}