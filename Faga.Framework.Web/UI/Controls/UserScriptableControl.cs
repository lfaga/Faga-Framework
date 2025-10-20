using System;
using System.Web.UI;
using Faga.Framework.Security.Model;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Web.UI.Controls
{
  public class UserScriptableControl : UserControl, IFrameworkPageController
  {
    private readonly FrameworkPageController _fpc;


    public UserScriptableControl()
    {
      _fpc = new FrameworkPageController(this);
      _fpc.ReturnedFromDialog += fpc_ReturnedFromDialog;
    }

    private void fpc_ReturnedFromDialog(object sender, UaDialogReturnEventArgs e)
    {
      if (ReturnedFromDialog != null)
      {
        ReturnedFromDialog(this, e);
      }
    }


    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      _fpc.OnInitProcess();
    }


    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      _fpc.OnPreRenderProcess();
    }

    #region IFrameworkPageController Members

    public event EventHandler<UaDialogReturnEventArgs> ReturnedFromDialog;


    public void IncludeScript(string filepath)
    {
      _fpc.IncludeScript(filepath);
    }


    public void AddOnLoadScriptLines(string line)
    {
      _fpc.AddOnLoadScriptLines(line);
    }


    public void AddOnAfterLoadScriptLines(string line)
    {
      _fpc.AddOnAfterLoadScriptLines(line);
    }


    public void AttachCssFile(string filepath)
    {
      _fpc.AttachCssFile(filepath);
    }


    public void ShowError(string message)
    {
      _fpc.ShowError(message);
    }


    public void ShowSuccess(string message)
    {
      _fpc.ShowSuccess(message);
    }


    public void ShowAlert(string title, string message)
    {
      _fpc.ShowAlert(title, message);
    }


    public void OpenDialog(string url, int height, int width)
    {
      _fpc.OpenDialog(url, height, width);
    }


    public void OpenWindow(string url, int height, int width, bool scrollbars)
    {
      _fpc.OpenWindow(url, height, width, scrollbars);
    }


    public void PostBack()
    {
      _fpc.PostBack();
    }


    public User CurrentUser
    {
      get { return _fpc.CurrentUser; }
    }

    #endregion
  }
}