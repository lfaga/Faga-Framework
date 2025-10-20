using System;
using Faga.Framework.Web.UI.Templates;

namespace Faga.Framework.Services.Administracion.Dialogs
{
  /// <summary>
  ///   Summary description for CrearParametro.
  /// </summary>
  public partial class CrearParametro : DialogTemplate
  {
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #region Web Form Designer generated code

    protected override void OnInit(EventArgs e)
    {
      //
      // CODEGEN: This call is required by the ASP.NET Web Form Designer.
      //
      InitializeComponent();
      base.OnInit(e);
    }


    /// <summary>
    ///   Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.Load += new EventHandler(this.Page_Load);
    }

    #endregion
  }
}