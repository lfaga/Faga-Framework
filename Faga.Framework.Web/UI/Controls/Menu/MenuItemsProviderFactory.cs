using System.Reflection;
using Faga.Framework.Web.Configuration;

namespace Faga.Framework.Web.UI.Controls.Menu
{
  public sealed class MenuItemsProviderFactory
  {
    private MenuItemsProviderFactory()
    {
    }


    public static IMenuItemsProvider MenuItems()
    {
      var path
        = WebApplicationConfiguration.WebUiConfiguration.MenuItemsProvider.Value;
      var a = Assembly.Load(path);
      var mip = (IMenuItemsProvider) a.CreateInstance(path + ".Menu.MenuItemsProvider");
      return mip;
    }
  }
}