using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Web.UI.Controls.Menu;
using Faga.Framework.Web.UI.Controls.Menu.Model;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  [ToolboxData("<{0}:SimpleMenuBar runat=server></{0}:SimpleMenuBar>")]
  public class SimpleMenuBar : ScriptableControl
  {
    private readonly HtmlTable _tblMenu;
    private readonly HtmlTableCell _tdContainer;


    public SimpleMenuBar()
    {
      _tblMenu = new HtmlTable();
      _tdContainer = new HtmlTableCell();
    }

    #region Overrides

    protected override void OnInit(EventArgs e)
    {
      SetupControls();

      base.OnInit(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
      LoadDbMenu();

      base.OnPreRender(e);
    }


    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      _tblMenu.RenderControl(htm);

      output.Write(str.ToString());
    }

    #endregion

    #region Private Methods

    private void SetupControls()
    {
      _tblMenu.Attributes["class"] = "menu";

      var tr = new HtmlTableRow();

      _tdContainer.Attributes["unselectable"] = "on";
      _tdContainer.Align = "left";
      _tdContainer.VAlign = "top";

      tr.Cells.Add(_tdContainer);

      _tblMenu.Rows.Add(tr);

      Controls.Add(_tblMenu);
    }


    private void LoadDbMenu()
    {
      var mnulst = MenuItemsProvider.List(new SimpleTransaction(CurrentUser));

      var doc = new XmlDocument();

      doc.LoadXml(
        @"<?xml version='1.0' encoding='utf-8' ?>
		<!DOCTYPE Menu [
			<!ELEMENT Menu ANY> 
			<!ELEMENT Folder ANY> 
			<!ELEMENT MenuItem EMPTY>
			<!ATTLIST Folder id ID #REQUIRED>
			<!ATTLIST MenuItem id ID #REQUIRED>
		]>
		<Menu>
		</Menu>");

      XmlElement xe;
      XmlAttribute xa;

      foreach (MenuItem item in mnulst)
      {
        XmlNode n;
        if (string.IsNullOrEmpty(item.Link))
        {
          xe = doc.CreateElement("Folder");

          xa = doc.CreateAttribute("id");
          xa.Value = "F" + item.Id;
          xe.Attributes.Append(xa);

          xa = doc.CreateAttribute("caption");
          xa.Value = item.Caption;
          xe.Attributes.Append(xa);

          n = item.IdParent > 0 ? doc.GetElementById("F" + item.IdParent) : doc.GetElementsByTagName("Menu")[0];
        }
        else
        {
          xe = doc.CreateElement("MenuItem");

          xa = doc.CreateAttribute("id");
          xa.Value = "I" + item.Id;
          xe.Attributes.Append(xa);

          xa = doc.CreateAttribute("caption");
          xa.Value = item.Caption;
          xe.Attributes.Append(xa);

          xa = doc.CreateAttribute("link");
          xa.Value = item.Link;
          xe.Attributes.Append(xa);

          n = item.IdParent > 0 ? doc.GetElementById("F" + item.IdParent) : doc.GetElementsByTagName("Menu")[0];
        }

        if (n != null)
          n.AppendChild(xe);
      }

      NavigateNode(doc.GetElementsByTagName("Menu")[0]);
    }


    private void NavigateNode(XmlNode node)
    {
      var enuEntries = node.ChildNodes.GetEnumerator();

      while (enuEntries != null && enuEntries.MoveNext())
      {
        var n = (XmlNode) enuEntries.Current;

        switch (n.Name)
        {
          case "Folder":

            if (n.Attributes != null && n.Attributes.Count > 0)
            {
              AddFolder(n);
            }

            if (n.HasChildNodes)
            {
              NavigateNode(n);
            }

            break;

          case "MenuItem":
            AddMenuItem(n);
            break;
        }
      }
    }


    private void AddFolder(XmlNode n)
    {
      var div = new HtmlGenericControl("DIV");
      div.Attributes["class"] = "menucaption";
      div.Attributes["unselectable"] = "on";
      if (n.Attributes != null) div.InnerText = n.Attributes["caption"].Value;

      _tdContainer.Controls.Add(div);
    }


    private void AddMenuItem(XmlNode n)
    {
      var a = new HtmlAnchor();
      a.Attributes["class"] = "menuitem";
      a.Attributes["unselectable"] = "on";
      if (n.Attributes != null)
      {
        a.HRef = n.Attributes["link"].Value;
        a.Target = "frmMain";
        a.InnerText = n.Attributes["caption"].Value;
      }

      _tdContainer.Controls.Add(a);
      _tdContainer.Controls.Add(new LiteralControl("<br>"));
    }

    #endregion
  }
}