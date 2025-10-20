using Faga.Framework.Data;
using Faga.Framework.Web.UI.Controls.Menu.Model;
using Faga.Framework.Web.UI.Controls.Menu.Model.Collections;

namespace Faga.Framework.Web.UI.Controls.Menu
{
  public interface IMenuItemsProvider
  {
    MenuItem GetItem(int id, IConnection cn);

    MenuItems List(IConnection cn);
  }
}