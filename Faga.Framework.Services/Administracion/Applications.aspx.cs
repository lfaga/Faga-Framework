using System;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Web.UI.Templates;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Services.Administracion
{
  /// <summary>
  ///   Summary description for Applications.
  /// </summary>
  public partial class ApplicationsPage
    : DataAbmTemplate<Application, ApplicationCollection>
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InitializeAbm(new ApplicationProvider(new UndoableTransaction(CurrentUser)),
          grdMaster, lnkNew, lnkCopy, lnkDelete, lnkSave, lnkCancel,
          lnkSearch, txtName, txtId);

        AddValidationScript("RequiredBox", "txtName", "Nombre de Aplicación");
      }
    }

    private void ApplicationsPage_MasterItemLoaded(object sender, UaMasterItemEventArgs<Application> e)
    {
      var app = e.Element;
      txtName.Text = app.Name;
      txtId.Text = app.Id.ToString();
    }


    private void ApplicationsPage_MasterItemBeforeSave(object sender, UaMasterItemEventArgs<Application> e)
    {
      var app = e.Element;
      app.Name = txtName.Text;
    }


    private void ApplicationsPage_MasterItemBeforeSearch(object sender, UaMasterItemEventArgs<Application> e)
    {
      var app = e.Element;

      if (txtId.Text != string.Empty)
      {
        app.Id = Convert.ToInt32(txtId.Text);
      }

      if (txtName.Text != string.Empty)
      {
        app.Name = txtName.Text;
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
      this.MasterItemBeforeSearch +=
        new EventHandler<UaMasterItemEventArgs<Application>>(this.ApplicationsPage_MasterItemBeforeSearch);
      this.MasterItemLoaded +=
        new EventHandler<UaMasterItemEventArgs<Application>>(this.ApplicationsPage_MasterItemLoaded);
      this.MasterItemBeforeSave +=
        new EventHandler<UaMasterItemEventArgs<Application>>(this.ApplicationsPage_MasterItemBeforeSave);
      this.Load += new EventHandler(this.Page_Load);
    }

    #endregion
  }
}