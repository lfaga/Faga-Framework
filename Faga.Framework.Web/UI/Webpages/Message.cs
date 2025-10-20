using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Faga.Framework.Web.Configuration;
using Faga.Framework.Web.UI.Templates;

namespace Faga.Framework.Web.UI.Webpages
{
  /// <summary>
  ///   Summary description for Message.
  /// </summary>
  public class Message : BasePageTemplate
  {
    protected Label LblMessage;
    protected Label LblTitle;
    protected HtmlAnchor LnkOk;


    protected override void OnLoad(EventArgs e)
    {
      SetupControls();

      if (!IsPostBack)
      {
        if (Request["text"] != null)
        {
          LblTitle.Text = Request["title"];
          LblMessage.Text = Request["text"];
          if (Request["link"] != null)
          {
            LnkOk.HRef = Request["link"];
          }
          else
          {
            LnkOk.Visible = false;
          }
        }
        else
        {
          var caller = (BasePageTemplate) Context.Handler;
          LblTitle.Text = caller.LoadMsgTitle;
          LblMessage.Text = caller.LoadMsgMessage;
          if (caller.LoadMsgLink != null)
          {
            LnkOk.HRef = caller.LoadMsgLink;
          }
          else
          {
            LnkOk.Visible = false;
          }
        }
      }

      base.OnLoad(e);
    }


    private void SetupControls()
    {
      var tblout = new HtmlTable();
      tblout.Width = "100%";
      tblout.Height = "100%";
      tblout.Border = 0;

      var trout = new HtmlTableRow();

      var tdout = new HtmlTableCell();
      tdout.Align = "center";
      tdout.VAlign = "middle";


      var tbl = new HtmlTable();
      tbl.Attributes["class"] = "window";
      tbl.Width = "50%";

      var tr = new HtmlTableRow();

      var td = new HtmlTableCell("TH");
      td.Align = "center";

      LblTitle = new Label();
      LblTitle.Text = "Error";

      td.Controls.Add(LblTitle);
      tr.Cells.Add(td);
      tbl.Rows.Add(tr);

      tr = new HtmlTableRow();

      td = new HtmlTableCell();
      td.Align = "center";

      LblMessage = new Label();

      td.Controls.Add(LblMessage);

      tr.Cells.Add(td);
      tbl.Rows.Add(tr);

      tr = new HtmlTableRow();
      tr.Attributes["class"] = "footer";

      td = new HtmlTableCell();
      td.Align = "right";

      LnkOk = new HtmlAnchor();
      LnkOk.Attributes["class"] = "button";
      LnkOk.Style["WIDTH"] = "80px";

      var img = new HtmlImage();
      img.Src
        = WebApplicationConfiguration.GetRelativePath("/images/icons/icon_ok.gif");

      LnkOk.InnerHtml = "Aceptar";

      LnkOk.Controls.AddAt(LnkOk.Controls.Count, img);

      td.Controls.Add(LnkOk);
      tr.Cells.Add(td);
      tbl.Rows.Add(tr);

      tdout.Controls.Add(tbl);
      trout.Cells.Add(tdout);
      tblout.Rows.Add(trout);

      Controls.Add(tblout);
    }
  }
}