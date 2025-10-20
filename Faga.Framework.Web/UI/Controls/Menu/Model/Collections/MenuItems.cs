using System;
using System.Collections;

namespace Faga.Framework.Web.UI.Controls.Menu.Model.Collections
{
  [Serializable]
  public class MenuItems : ArrayList
  {
    public new MenuItem this[int index]
    {
      get { return (MenuItem) base[index]; }
      set { base[index] = value; }
    }

    public void Add(MenuItem menuItem)
    {
      base.Add(menuItem);
    }
  }
}