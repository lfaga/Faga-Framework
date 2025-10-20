using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Web.Configuration;
using Faga.Framework.Web.UI.Controls.Menu;
using Faga.Framework.Web.UI.Controls.Menu.Model;

namespace Faga.Framework.Web.UI.Controls.WebControls
{
  [ToolboxData("<{0}:TreeNavControl runat=server></{0}:TreeNavControl>")]
  public class TreeNavControl : ScriptableControl
  {
    protected HtmlGenericControl NavTreeContainer;
    protected HtmlTable TblOutter;

    public string XmlPath { get; set; }

    #region Overrides

    protected override void OnInit(EventArgs e)
    {
      SetupControls();

      base.OnInit(e);
    }


    protected override void OnLoad(EventArgs e)
    {
      AttachCssFile("/treenavcontrol.css");
      IncludeScript("/java/ui/controls/TreeNav.pjs");
      AddOnLoadScriptLines("page.addControl( new TreeNav('" + UniqueID + "') );");

      base.OnLoad(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
      if (MenuItemsProviderFactory.MenuItems() != null)
      {
        LoadDbMenu();
      }
      else if (XmlPath != null)
      {
        LoadXml(XmlPath);
      }

      base.OnPreRender(e);
    }


    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      TblOutter.RenderControl(htm);

      output.Write(str.ToString());
    }

    #endregion

    #region Private Methods

    private void SetupControls()
    {
      TblOutter = new HtmlTable();
      TblOutter.ID = ClientID;
      TblOutter.Width = "100%";
      TblOutter.Height = "100%";
      TblOutter.Border = 0;
      TblOutter.CellPadding = 0;
      TblOutter.CellSpacing = 0;

      var tr = new HtmlTableRow();

      var td = new HtmlTableCell();
      td.Attributes["class"] = "tree";

      NavTreeContainer = new HtmlGenericControl("DIV");
      NavTreeContainer.Attributes["unselectable"] = "on";
      NavTreeContainer.Attributes["class"] = "treecontents";

      if (Width.IsEmpty)
      {
        NavTreeContainer.Style["WIDTH"] = "240px";
      }
      else
      {
        NavTreeContainer.Style["WIDTH"] = Width.ToString();
      }

      NavTreeContainer.ID = ClientID + "_navTreeContainer";

      td.Controls.Add(NavTreeContainer);

      tr.Cells.Add(td);

      td = new HtmlTableCell();
      td.Style["WIDTH"] = "15px";
      td.Align = "right";
      td.VAlign = "top";

      var a = new HtmlAnchor();
      a.HRef = "#";
      a.ID = ClientID + "_lnkNavTreeToggle";

      var img = new HtmlImage();
      img.Src = WebApplicationConfiguration.GetRelativePath(
        "/images/icons/button_contract.gif");
      img.Border = 0;
      img.ID = ClientID + "_imgNavTreeWidget";
      img.Style["MARGIN"] = "0px 1px 0px 2px";

      a.Controls.Add(img);
      td.Controls.Add(a);

      tr.Cells.Add(td);

      TblOutter.Rows.Add(tr);
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

      foreach (MenuItem item in mnulst)
      {
        XmlElement xe;
        XmlAttribute xa;

        if (item.Link != null)
        {
          xe = doc.CreateElement("Folder");

          xa = doc.CreateAttribute("id");
          xa.Value = string.Concat("F", item.Id);
          xe.Attributes.Append(xa);

          xa = doc.CreateAttribute("caption");
          xa.Value = item.Caption;
          xe.Attributes.Append(xa);

          XmlNode n;

          if (item.IdParent > 0)
          {
            n = doc.GetElementById(string.Concat("F", item.IdParent));
          }
          else
          {
            n = doc.GetElementsByTagName("Menu")[0];
          }

          n.AppendChild(xe);
        }
        else
        {
          xe = doc.CreateElement("MenuItem");

          xa = doc.CreateAttribute("id");
          xa.Value = string.Concat("I" + item.Id);
          xe.Attributes.Append(xa);

          xa = doc.CreateAttribute("caption");
          xa.Value = item.Caption;
          xe.Attributes.Append(xa);

          xa = doc.CreateAttribute("link");
          xa.Value = item.Link;
          xe.Attributes.Append(xa);

          XmlNode n;

          if (item.IdParent > 0)
          {
            n = doc.GetElementById(string.Concat("F", item.IdParent));
          }
          else
          {
            n = doc.GetElementsByTagName("Menu")[0];
          }

          n.AppendChild(xe);
        }
      }

      NavigateNode(doc.GetElementsByTagName("Menu")[0], NavTreeContainer, 1);
    }


    private void LoadXml(string filename)
    {
      var doc = new XmlDocument();

      doc.Load(filename);

      NavigateNode(doc.GetElementsByTagName("Menu")[0], NavTreeContainer, 1);
    }


    private void NavigateNode(XmlNode node, HtmlGenericControl divCont, int level)
    {
      var enuEntries = node.ChildNodes.GetEnumerator();

      while (enuEntries.MoveNext())
      {
        var n = (XmlNode) enuEntries.Current;

        switch (n.Name)
        {
          case "Folder":
            HtmlGenericControl divContNext;
            var l = level;

            if (n.Attributes.Count > 0)
            {
              divContNext = AddFolder(n, divCont, level);
            }
            else
            {
              l -= 1;
              divContNext = divCont;
            }

            if (n.HasChildNodes)
            {
              NavigateNode(n, divContNext, l + 1);
            }

            break;

          case "MenuItem":
            AddMenuItem(n, divCont, level);
            break;
        }
        divCont.Controls.Add(new LiteralControl(Environment.NewLine));
      }
    }


    private HtmlGenericControl AddFolder(XmlNode n, HtmlGenericControl divCont,
      int level)
    {
      var sFldId = "fld" + n.Attributes["id"].Value;

      var div = new HtmlGenericControl("DIV");
      div.Attributes["class"] = string.Concat("navtreelevel", level);
      div.Attributes["unselectable"] = "on";

      var a = new HtmlAnchor();
      a.HRef = "#";
      a.Attributes["class"] = "folder";
      a.Attributes["unselectable"] = "on";
      a.Attributes["onClick"] = string.Concat(
        "try { ", ClientID, ".controllerObject.ToggleFolder('",
        sFldId, "'); } catch (e) {} return (false);");

      var img = new HtmlImage();
      img.ID = ClientID + "_imgW" + sFldId;
      img.Src = WebApplicationConfiguration.GetRelativePath(
        "/images/icons/tree_button_expand.gif");
      img.Border = 0;
      img.Align = "absMiddle";
      img.Style["MARGIN-RIGHT"] = "2px";
      a.Controls.Add(img);

      img = new HtmlImage();
      img.ID = ClientID + "_imgF" + sFldId;
      img.Src = WebApplicationConfiguration.GetRelativePath(
        "/images/icons/icon_folder_closed.gif");
      img.Border = 0;
      img.Align = "absMiddle";
      img.Style["MARGIN-RIGHT"] = "4px";
      a.Controls.Add(img);

      var lc = new LiteralControl(n.Attributes["caption"].Value);
      a.Controls.Add(lc);


      div.Controls.AddAt(0, a);

      var divItems = new HtmlGenericControl("DIV");
      divItems.ID = ClientID + "_" + sFldId;
      divItems.Attributes["class"] = "navtreeitemcontainer";
      divItems.Style["visibility"] = "hidden";
      divItems.Style["display"] = "none";

      divCont.Controls.Add(div);
      divCont.Controls.Add(divItems);

      return divItems;
    }


    private void AddMenuItem(XmlNode n, HtmlGenericControl divCont, int level)
    {
      var div = new HtmlGenericControl("DIV");
      div.Attributes["class"] = string.Concat("navtreelevel", level);
      div.Attributes["unselectable"] = "on";

      var img = new HtmlImage();
      img.Src = WebApplicationConfiguration.GetRelativePath(
        "/images/spacer.gif");
      img.Border = 0;
      img.Align = "absMiddle";
      img.Style["MARGIN-RIGHT"] = "2px";
      img.Style["WIDTH"] = "16px";
      img.Style["HEIGHT"] = "16px";
      div.Controls.AddAt(0, img);

      var a = new HtmlAnchor();
      a.HRef = n.Attributes["link"].Value;
      a.Target = "frmMain";
      a.Attributes["onClick"] = string.Format(
        "return ({0}.controllerObject.ItemOnClick());", ClientID);
      a.InnerText = n.Attributes["caption"].Value;

      img = new HtmlImage();
      img.Src = WebApplicationConfiguration.GetRelativePath(
        "/images/icons/icon_transaction.gif");
      img.Border = 0;
      img.Align = "absMiddle";
      img.Style["MARGIN-RIGHT"] = "4px";
      a.Controls.AddAt(0, img);

      div.Controls.AddAt(1, a);

      divCont.Controls.Add(div);
    }

    #endregion
  }
}