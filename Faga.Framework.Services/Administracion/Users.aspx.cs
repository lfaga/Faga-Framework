using System;
using System.Web.UI.WebControls;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Exceptions;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Security.UserData;
using Faga.Framework.Web.UI.Templates;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Services.Administracion.Webpages
{
  /// <summary>
  ///   Summary description for Applications.
  /// </summary>
  public partial class UsersPage
    : DataAbmTemplate<User, UserCollection>
  {
    protected Label Label1;


    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        InitializeAbm(new UserProvider(new UndoableTransaction(CurrentUser)),
          grdMaster, lnkNew, lnkCopy, lnkDelete, lnkSave, lnkCancel,
          lnkSearch, txtUserName, txtNombre, txtApellido,
          txtTel, txtEmail, lnkGetUserData);

        AddValidationScript("RequiredBox", "txtUserName", "Nombre de Usuario");

        AddValidationScript("RequiredBox", "txtNombre", "Nombre");

        AddValidationScript("RequiredBox", "txtApellido", "Apellido");

        AddValidationScript("RequiredBox", "txtEmail", "E-mail");

        if (!UserDataProvider.IsUserDataProviderPresent())
        {
          lnkGetUserData.CssClass = "invisible";
        }
      }
    }

    private void UsersPage_MasterItemLoaded(object sender, UaMasterItemEventArgs<User> e)
    {
      var u = e.Element;
      txtUserName.Text = u.UserName;
      txtNombre.Text = u.Nombre;
      txtApellido.Text = u.Apellido;
      txtEmail.Text = u.Email;
      txtTel.Text = u.Telefono;
    }


    private void UsersPage_MasterItemBeforeSave(object sender, UaMasterItemEventArgs<User> e)
    {
      var u = e.Element;
      u.UserName = txtUserName.Text;
      u.Nombre = txtNombre.Text;
      u.Apellido = txtApellido.Text;
      u.Email = txtEmail.Text;
      u.Telefono = txtTel.Text;
    }


    private void UsersPage_MasterItemBeforeSearch(object sender, UaMasterItemEventArgs<User> e)
    {
      var u = e.Element;
      if (txtUserName.Text != string.Empty)
      {
        u.UserName = txtUserName.Text;
      }
      if (txtNombre.Text != string.Empty)
      {
        u.Nombre = txtNombre.Text;
      }
      if (txtApellido.Text != string.Empty)
      {
        u.Apellido = txtApellido.Text;
      }
      if (txtEmail.Text != string.Empty)
      {
        u.Email = txtEmail.Text;
      }
      if (txtTel.Text != string.Empty)
      {
        u.Telefono = txtTel.Text;
      }
    }


    protected void lnkGetUserData_Click(object sender, EventArgs e)
    {
      try
      {
        if (txtUserName.Text != string.Empty)
        {
          var u = UserDataProvider.FillUserData(txtUserName.Text);

          txtNombre.Text = u.Nombre;
          txtApellido.Text = u.Apellido;
          txtEmail.Text = u.Email;
          txtTel.Text = u.Telefono;
        }
        else
        {
          throw new ArgumentException("Debe ingresar un nombre de usuario.");
        }
      }
      catch (UserDataProviderException ex)
      {
        ShowError(ex.Message);
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
        new EventHandler<UaMasterItemEventArgs<User>>(
          this.UsersPage_MasterItemBeforeSearch);
      this.MasterItemLoaded +=
        new EventHandler<UaMasterItemEventArgs<User>>(this.UsersPage_MasterItemLoaded);
      this.MasterItemBeforeSave +=
        new EventHandler<UaMasterItemEventArgs<User>>(this.UsersPage_MasterItemBeforeSave);
    }

    #endregion
  }
}