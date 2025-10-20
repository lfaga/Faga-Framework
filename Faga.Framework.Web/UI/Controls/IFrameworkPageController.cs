using System;
using Faga.Framework.Security.Model;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Web.UI.Controls
{
  public interface IFrameworkPageController
  {
    User CurrentUser { get; }
    event EventHandler<UaDialogReturnEventArgs> ReturnedFromDialog;

    void IncludeScript(string filepath);

    void AddOnLoadScriptLines(string line);

    void AddOnAfterLoadScriptLines(string line);

    void AttachCssFile(string filepath);

    void ShowError(string message);

    void ShowSuccess(string message);

    void ShowAlert(string title, string message);

    void OpenDialog(string url, int height, int width);

    void OpenWindow(string url, int height, int width, bool scrollbars);

    void PostBack();
  }
}