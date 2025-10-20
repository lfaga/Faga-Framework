using System;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Web.UI.Controls;
using Faga.Framework.Web.UI.Templates;

namespace Faga.Framework.Services.Administracion.Dialogs
{
  /// <summary>
  ///   Summary description for seleccionarAtributo.
  /// </summary>
  public partial class SeleccionarGrupo : DialogTemplate
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        var trans = new SimpleTransaction(CurrentUser);
        trans.IdApplication = Convert.ToInt32(Request["application"]);

        var g = new Group();
        g.IdApplication = trans.IdApplication;

        ListControls.Fill(cboGrupo, new GroupProvider(trans).List(g),
          "Name", "Id", "[Grupo]");
      }
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