using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Web.UI.Templates
{
  /// <summary>
  ///   Summary description for PageTemplate.
  /// </summary>
  public class PageTemplate : BasePageTemplate
  {
    private ArrayList _onAfterLoadScriptLines;
    private ArrayList _onLoadScriptLines;

    public event EventHandler<UaDialogReturnEventArgs> ReturnedFromDialog;

    #region Overrides

    protected override void OnLoad(EventArgs e)
    {
      if (IsPostBack)
      {
        var s = Request["__hidDialogReturn"];
        var u = Request["__hidDialogURL"];

        if ((s != null)
            && (s != string.Empty)
            && (ReturnedFromDialog != null))
        {
          ReturnedFromDialog(this, new UaDialogReturnEventArgs(u, ParseReturnValue(s)));
        }
      }

      base.OnLoad(e);
    }


    protected override void Render(HtmlTextWriter output)
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      SetupPage();

      base.Render(htm);

      output.Write(str.ToString());
    }

    #endregion

    #region Private Methods

    private void SetupPage()
    {
      Header.Controls.Add(new LiteralControl(GetOnLoadScript()));

      ClientScript.RegisterHiddenField("__hidDialogReturn", "");
      ClientScript.RegisterHiddenField("__hidDialogURL", "");

      var st = string.Empty;

      if (Request["__hidScrollTop"] != null)
      {
        st = Request["__hidScrollTop"];
      }

      ClientScript.RegisterHiddenField("__hidScrollTop", st);
    }


    private string GetOnLoadScript()
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      var scp = new HtmlGenericControl("SCRIPT");
      scp.Attributes["language"] = "javascript";
      scp.Attributes["type"] = "text/javascript";

      var sb = new StringBuilder();

      sb.Append("\n<!--\n\t\tvar page = new Page(window.document);\n");
      sb.Append("\n\t\tfunction pageOnLoad()\n\t\t{");

      foreach (string s in OnLoadScriptLines)
      {
        sb.AppendFormat("\n\t\t	{0}", s);
      }

      sb.Append("\n\n\t\t	page.onLoad();\n\t\t}");
      sb.AppendFormat("\n\n\t\t{0}//-->\n\n", GetOnAfterLoadScript());

      scp.InnerHtml = sb.ToString();

      scp.RenderControl(htm);

      return str.ToString();
    }


    private string GetOnAfterLoadScript()
    {
      var sb = new StringBuilder();

      if (OnAfterLoadScriptLines.Count > 0)
      {
        sb.Append("\n\t\tfunction AfterLoadServerEvent()\n\t\t{");

        foreach (string sev in OnAfterLoadScriptLines)
        {
          sb.AppendFormat("\n\t\t\t{0}", sev);
        }

        sb.Append("\n\t\t}\n\n");
      }

      return sb.ToString();
    }


    private static Dictionary<string, string> ParseReturnValue(string retval)
    {
      var ret = new Dictionary<string, string>();

      if (!retval.EndsWith(";"))
      {
        retval += ";";
      }

      var sa = retval.Split(";".ToCharArray());

      foreach (var s in sa)
      {
        var sp = s.Split("=".ToCharArray());
        if (sp[0] != string.Empty)
        {
          ret.Add(sp[0], sp[1]);
        }
      }

      return ret;
    }

    #endregion

    #region Public Methods

    public void AddOnLoadScriptLines(string scp)
    {
      if (!OnLoadScriptLines.Contains(scp))
      {
        OnLoadScriptLines.Add(scp);
      }
    }


    public void AddOnAfterLoadScriptLines(string scp)
    {
      if (!OnAfterLoadScriptLines.Contains(scp))
      {
        OnAfterLoadScriptLines.Add(scp);
      }
    }

    public void SetButtonCausesValidation(Control ctl)
    {
      if (ctl != null)
      {
        IncludeScript("/java/ui/controls/LinkButton.pjs");

        AddOnLoadScriptLines(string.Format("page.addControl( new LinkButton('{0}') );", ctl.ClientID));

        AddOnLoadScriptLines(@"page.Controls.Items['"
                             + ctl.ClientID +
                             @"'].onClick = function(source, eventargs)
				{
					if ( source.page.isValid() )
						return (true);
					else
						page.StatusBar.flash();
				}");
      }
    }


    public void ShowError(string message)
    {
      AddOnAfterLoadScriptLines(
        string.Format("document.controllerObject.StatusBar.ShowError('{0}');",
          GetSafeStatusBarString(message)));
    }


    public void ShowSuccess(string message)
    {
      AddOnAfterLoadScriptLines(
        string.Format("document.controllerObject.StatusBar.ShowSuccess('{0}');",
          GetSafeStatusBarString(message)));
    }


    public void ShowAlert(string title, string message)
    {
      AddOnAfterLoadScriptLines(
        string.Format("document.controllerObject.ShowAlertMessage('{0}', '{1}');",
          title, message));
    }


    public void OpenDialog(string url, int height, int width)
    {
      var sb = new StringBuilder();

      sb.AppendFormat("var dop = new DialogOpener();\ndop.url = '{0}';\n", url);
      sb.AppendFormat("dop.height = '{0}px';\ndop.width = '{1}px';", height, width);
      sb.Append(
        @"dop.contents = null;
		dop.Show();
		if ( (dop.returnValue != undefined)
			&& (dop.returnValue != null) )
		{
			document.getElementById('__hidDialogURL').value = dop.url;
			document.getElementById('__hidDialogReturn').value = dop.returnValue;
			__doPostBack('','');
		}");

      AddOnAfterLoadScriptLines(sb.ToString());
    }


    public void OpenWindow(string url, int height, int width, bool scrollbars)
    {
      var sb = new StringBuilder();

      sb.AppendFormat("x=(screen.width/2-({0}/2));\n", width);
      sb.AppendFormat("y=(screen.height/2-({0}/2));\n", height);

      sb.AppendFormat("window.open('{0}', null,'toolbar=0,scrollbars={1},location=0,",
        url, scrollbars ? 1 : 0);

      sb.AppendFormat("statusbar=0,menubar=0,resizable=0,width={0},", width);
      sb.AppendFormat("height={0},left=' + x + ',top= ' + y);", height);

      AddOnAfterLoadScriptLines(sb.ToString());
    }


    public void FocusControl(Control control)
    {
      AddOnLoadScriptLines(
        string.Format("document.getElementById('{0}').focus();", control.ClientID));
    }


    public void RestoreScroll()
    {
      AddOnLoadScriptLines("page.RestoreScroll();");
    }


    public void ScrollToControl(Control control)
    {
      AddOnLoadScriptLines(
        string.Format("page.ScrollToControl('{0}');", control.ClientID));
    }


    public void PostBack()
    {
      AddOnLoadScriptLines("page.PostBack();");
    }

    #endregion

    #region Private Fields

    private ArrayList OnLoadScriptLines
    {
      get
      {
        if (_onLoadScriptLines == null)
        {
          _onLoadScriptLines = new ArrayList();
        }

        return _onLoadScriptLines;
      }
    }


    private ArrayList OnAfterLoadScriptLines
    {
      get
      {
        if (_onAfterLoadScriptLines == null)
        {
          _onAfterLoadScriptLines = new ArrayList();
        }

        return _onAfterLoadScriptLines;
      }
    }

    #endregion
  }
}