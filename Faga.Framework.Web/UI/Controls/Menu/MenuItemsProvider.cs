using Faga.Framework.Data.Transactions;
using Faga.Framework.Web.UI.Controls.Menu.Model;
using Faga.Framework.Web.UI.Controls.Menu.Model.Collections;

namespace Faga.Framework.Web.UI.Controls.Menu
{
  public sealed class MenuItemsProvider
  {
    public static MenuItem GetItem(int id, BaseTransaction trans)
    {
      return MenuItemsProviderFactory.MenuItems().GetItem(id,
        trans.Connection);
    }


    public static MenuItems List(BaseTransaction trans)
    {
      return MenuItemsProviderFactory.MenuItems().List(trans.Connection);
    }
  }
}