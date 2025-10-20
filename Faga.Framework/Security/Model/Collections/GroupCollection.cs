using System;
using System.Collections.ObjectModel;

namespace Faga.Framework.Security.Model.Collections
{
  [Serializable]
  public class GroupCollection : Collection<Group>
  {
    public new bool Contains(Group group)
    {
      foreach (var g in this)
      {
        if ((g.Id == group.Id)
            && (g.IdApplication == group.IdApplication))
        {
          return true;
        }
      }
      return false;
    }
  }
}