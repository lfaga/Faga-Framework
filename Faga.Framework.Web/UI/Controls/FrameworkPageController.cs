using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.UI;
using Faga.Framework.Security.Model;
using Faga.Framework.Web.UI.Templates;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Web.UI.Controls
{
  public class FrameworkPageController : IFrameworkPageController
  {
    private readonly StringCollection _cssFiles;
    private readonly StringCollection _includeScripts;
    private readonly StringCollection _onAfterLoadScriptLines;
    private readonly StringCollection _onLoadScriptLines;
    private readonly Control _parent;


    public FrameworkPageController(Control control)
    {
      _parent = control;

      _includeScripts = new StringCollection();
      _onLoadScriptLines = new StringCollection();
      _onAfterLoadScriptLines = new StringCollection();
      _cssFiles = new StringCollection();
    }

    public void OnInitProcess()
    {
      var pt = _parent.Page as PageTemplate;
      if (pt != null)
      {
        pt.ReturnedFromDialog += pt_ReturnedFromDialog;
      }
      else
      {
        if ((_onAfterLoadScriptLines.Count > 0)
            || (_onLoadScriptLines.Count > 0))
        {
          throw new InvalidCastException("Page is not PageTemplate");
        }
      }
    }


    public void OnPreRenderProcess()
    {
      var bpt = _parent.Page as BasePageTemplate;
      var pt = _parent.Page as PageTemplate;

      if (bpt != null)
      {
        foreach (var include in _includeScripts)
        {
          bpt.IncludeScript(include);
        }

        foreach (var cssFile in _cssFiles)
        {
          bpt.AttachCssFile(cssFile);
        }
      }
      else
      {
        if ((_includeScripts.Count > 0)
            || (_cssFiles.Count > 0))
        {
          throw new InvalidCastException("Page is not BasePageTemplate");
        }
      }

      if (pt != null)
      {
        foreach (var line in _onAfterLoadScriptLines)
        {
          pt.AddOnAfterLoadScriptLines(line);
        }

        foreach (var line in _onLoadScriptLines)
        {
          pt.AddOnLoadScriptLines(line);
        }
      }
      else
      {
        if ((_onAfterLoadScriptLines.Count > 0)
            || (_onLoadScriptLines.Count > 0))
        {
          throw new InvalidCastException("Page is not PageTemplate");
        }
      }
    }


    private void pt_ReturnedFromDialog(object sender, UaDialogReturnEventArgs e)
    {
      if (ReturnedFromDialog != null)
      {
        ReturnedFromDialog(this, e);
      }
    }


    private string GetSafeStatusBarString(string s)
    {
      var r = HttpContext.Current.Server.HtmlEncode(s);
      r = r.Replace("'", "´");
      r = r.Replace("\n", " ");
      r = r.Replace("\r", " ");
      return r;
    }

    #region IFrameworkPageController Members

    public event EventHandler<UaDialogReturnEventArgs> ReturnedFromDialog;


    public void IncludeScript(string filepath)
    {
      if (!_includeScripts.Contains(filepath))
      {
        _includeScripts.Add(filepath);
      }
    }


    public void AddOnLoadScriptLines(string line)
    {
      if (!_onLoadScriptLines.Contains(line))
      {
        _onLoadScriptLines.Add(line);
      }
    }


    public void AddOnAfterLoadScriptLines(string line)
    {
      if (!_onAfterLoadScriptLines.Contains(line))
      {
        _onAfterLoadScriptLines.Add(line);
      }
    }


    public void AttachCssFile(string filepath)
    {
      if (!_cssFiles.Contains(filepath))
      {
        _cssFiles.Add(filepath);
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


    public void PostBack()
    {
      AddOnLoadScriptLines("page.PostBack();");
    }


    public User CurrentUser
    {
      get
      {
        if (_parent.Page is BasePageTemplate)
        {
          return (_parent.Page as BasePageTemplate).CurrentUser;
        }
        throw new InvalidCastException("Page is not BasePageTemplate");
      }
    }

    #endregion
  }
}