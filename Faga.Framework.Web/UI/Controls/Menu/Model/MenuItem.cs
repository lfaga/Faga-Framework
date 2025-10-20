using System;

namespace Faga.Framework.Web.UI.Controls.Menu.Model
{
  [Serializable]
  public class MenuItem
  {
    public MenuItem()
    {
      Id = int.MinValue;
      Caption = null;
      IdParent = int.MinValue;
      Link = null;
    }


    public MenuItem(int id, string caption, int idParent, string link)
    {
      Id = id;
      Caption = caption;
      IdParent = idParent;
      Link = link;
    }

    #region Public Properties

    public int Id { get; set; }

    public string Caption { get; set; }

    public int IdParent { get; set; }

    public string Link { get; set; }

    #endregion
  }
}