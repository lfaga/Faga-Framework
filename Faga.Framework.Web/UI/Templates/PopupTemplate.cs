namespace Faga.Framework.Web.UI.Templates
{
  /// <summary>
  ///   Summary description for PopupTemplate.
  /// </summary>
  public class PopupTemplate : PageTemplate
  {
    #region Protected Methods

    protected void Close()
    {
      AddOnLoadScriptLines("window.close();");
    }


    protected void PostBackOpener()
    {
      AddOnLoadScriptLines("opener.document.controllerObject.PostBack();");
    }

    #endregion
  }
}