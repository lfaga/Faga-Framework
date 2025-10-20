using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Faga.Framework.Configuration;
using Faga.Framework.Security;
using Faga.Framework.Security.Exceptions;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Transactions;
using Faga.Framework.Web.Configuration;

namespace Faga.Framework.Web.UI.Templates
{
  public class BasePageTemplate : Page
  {
    private ArrayList _cssFiles;
    private ArrayList _includeFiles;

    private User _usuario;

    #region Overrides

    protected override void OnPreInit(EventArgs e)
    {
      Theme = WebApplicationConfiguration.WebUiConfiguration.ThemeName;
      base.OnPreInit(e);
    }


    protected override void OnInit(EventArgs e)
    {
      foreach (GenericConfigurationElement<string> element in 
        WebApplicationConfiguration.WebUiConfiguration.DefaultJavaScriptIncludes)
      {
        IncludeScript(element.Value);
      }

      base.OnInit(e);
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

    #region Protected Methods

    protected void LoadMessagePage(string title, string message)
    {
      LoadMessagePage(title, message, null);
    }


    protected void LoadMessagePage(string title, string message, string link)
    {
      LoadMsgTitle = title;
      LoadMsgMessage = message;
      LoadMsgLink = link;
      Server.Transfer(WebApplicationConfiguration.GetRelativePath("/message.aspx"));
    }


    protected string GetSafeStatusBarString(string s)
    {
      var r = Server.HtmlEncode(s);
      r = r.Replace("'", "´");
      r = r.Replace("\n", " ");
      r = r.Replace("\r", " ");
      return r;
    }

    #endregion

    #region Public Methods

    public void IncludeScript(string filepath)
    {
      var s = WebApplicationConfiguration.GetRelativePath(filepath);

      if (!Includes.Contains(s))
      {
        Includes.Add(s);
      }
    }


    public void AttachCssFile(string filepath)
    {
      var s = WebApplicationConfiguration.GetRelativePath(filepath);

      if (!CssFiles.Contains(s))
      {
        CssFiles.Add(s);
      }
    }


    public void ExceptionHandling(Exception ex)
    {
      var sb = new StringBuilder();

      if (typeof (AccessDeniedException).IsInstanceOfType(ex))
      {
        sb.AppendFormat("<br><img src='{0}/images/accessdenied.gif'>",
          WebApplicationConfiguration.GetBaseUrl());
        sb.Append("<br>Acceso Denegado.<hr>");
        sb.AppendFormat("{0} : {1}<br>&nbsp;", ex.Source, ex.Message);

        LoadMessagePage("Acceso Denegado", sb.ToString());
      }
      else if (typeof (Exception).IsInstanceOfType(ex))
      {
        sb.AppendFormat("<br><img src='{0}/images/error.gif'>",
          WebApplicationConfiguration.GetBaseUrl());
        sb.Append("<br>Ha ocurrido un error.<hr>");
        sb.AppendFormat("{0} : {1}", ex.Source, ex.Message);
        sb.AppendFormat("<br><small style='white-space:nowrap;'>{0}</small>&nbsp;",
          ex.StackTrace.Replace("  ", " "
            ).Replace(" in ", "\nin "
            ).Replace("\n", "<br>"));

        LoadMessagePage("ERROR", sb.ToString());
      }
    }

    #endregion

    #region Private Methods

    private void SetupPage()
    {
      var lc = new LiteralControl(
        string.Concat(GetCssInclude(), GetIncludeList()));

      Header.Controls.AddAt(0, lc);
    }


    private string GetCssInclude()
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      foreach (string s in CssFiles)
      {
        var css = new HtmlGenericControl("LINK");
        css.Attributes.Add("rel", "stylesheet");
        css.Attributes.Add("type", "text/css");
        css.Attributes.Add("href", s);

        css.RenderControl(htm);

        str.Write(Environment.NewLine);
      }

      return str.ToString();
    }


    private string GetIncludeList()
    {
      var str = new StringWriter();
      var htm = new HtmlTextWriter(str);

      foreach (string s in Includes)
      {
        var scp = new HtmlGenericControl("SCRIPT");
        scp.Attributes["language"] = "javascript";
        scp.Attributes["type"] = "text/javascript";

        scp.Attributes["src"] = WebApplicationConfiguration.GetRelativePath(s);

        scp.RenderControl(htm);
        str.Write(Environment.NewLine);
      }
      return str.ToString();
    }

    #endregion

    #region Private Fields

    private ArrayList Includes
    {
      get
      {
        if (_includeFiles == null)
        {
          _includeFiles = new ArrayList();
        }

        return _includeFiles;
      }
    }

    private ArrayList CssFiles
    {
      get
      {
        if (_cssFiles == null)
        {
          _cssFiles = new ArrayList();
        }

        return _cssFiles;
      }
    }

    #endregion

    #region Public Fields

    public User CurrentUser
    {
      get
      {
        try
        {
          if ((_usuario == null) && Page.User.Identity.IsAuthenticated)
          {
            using (var st = new SecurityTransaction(Page.User))
            {
              using (var up = new UserProvider(st))
              {
                _usuario = up.Logon();
              }
            }
          }
        }
        catch (Exception ex)
        {
          ExceptionHandling(ex);
        }

        return _usuario;
      }
    }


    public string LoadMsgTitle { get; private set; }


    public string LoadMsgMessage { get; private set; }


    public string LoadMsgLink { get; private set; }

    #endregion
  }
}