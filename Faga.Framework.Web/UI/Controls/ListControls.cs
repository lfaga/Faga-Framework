using System.Data;
using System.Web.UI.WebControls;

namespace Faga.Framework.Web.UI.Controls
{
  public class ListControls
  {
    private ListControls()
    {
    }


    public static void Fill(ListControl lc, object col)
    {
      Fill(lc, col, null);
    }


    public static void Fill(ListControl lc, object col, string unselecteditem)
    {
      Fill(lc, col, "Text", "Value", unselecteditem);
    }


    public static void Fill(ListControl lc, object col,
      string sTextField, string sValueField)
    {
      Fill(lc, col, sTextField, sValueField, null);
    }


    public static void Fill(ListControl lc, object col, string sTextField,
      string sValueField, string unselectedItem)
    {
      lc.DataSource = col;
      lc.DataTextField = sTextField;
      lc.DataValueField = sValueField;
      lc.DataBind();

      if ((unselectedItem != null)
          && (unselectedItem != ""))
      {
        lc.Items.Insert(0, new ListItem(unselectedItem));
      }
    }


    public static void Fill(ListControl lc, DataView dv)
    {
      Fill(lc, dv, null);
    }


    public static void Fill(ListControl lc, DataView dv, string unselectedItem)
    {
      lc.DataSource = dv;
      lc.DataTextField = "Text";
      lc.DataValueField = "Value";
      lc.DataBind();

      if (unselectedItem != null)
      {
        lc.Items.Insert(0, unselectedItem);
      }

      if (lc.Items.Count > 0)
      {
        lc.SelectedIndex = 0;
      }
    }


    public static void SelectByValue(ListControl lc, int valor)
    {
      SelectByValue(lc, valor.ToString(), false);
    }


    public static void SelectByValue(ListControl lc, string valor)
    {
      SelectByValue(lc, valor, false);
    }


    public static void SelectByValue(ListControl lc, int valor, bool defaultFirst)
    {
      SelectByValue(lc, valor.ToString(), defaultFirst);
    }


    public static void SelectByValue(ListControl lc, string valor, bool defaultFirst)
    {
      var enu = lc.Items.GetEnumerator();
      ListItem li;

      if (!defaultFirst)
      {
        lc.SelectedIndex = -1;
      }
      else if (lc.Items.Count > 0)
      {
        lc.SelectedIndex = 0;
      }

      while (enu.MoveNext())
      {
        li = (ListItem) enu.Current;
        li.Selected = false;

        if (li.Value == valor)
        {
          li.Selected = true;
          break;
        }
      }
    }
  }
}