using System;
using System.Collections.ObjectModel;

namespace Faga.Framework.Security.Model.Collections
{
  [Serializable]
  public class PermissionCollection : Collection<Permission>
  {
    public new bool Contains(Permission permission)
    {
      if (permission != null)
      {
        foreach (var p in this)
        {
          if ((p.Id == permission.Id)
              && (p.IdApplication == permission.IdApplication))
          {
            return true;
          }
        }
      }
      return false;
    }
  }
}