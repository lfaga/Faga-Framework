using System;
using System.Web.UI.WebControls;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Web.UI.Controls;
using Faga.Framework.Web.UI.Templates;
using Faga.Framework.Web.UI.Templates.Events;

namespace Faga.Framework.Services.Administracion.Webpages
{
  /// <summary>
  ///   Summary description for Applications.
  /// </summary>
  public partial class UsersGroupsPage
    : DataAbmTemplate<User, UserCollection>
  {
    private readonly string seleccionarURL = "/Administracion/Dialogs/SeleccionarGrupo.aspx?application={0}";


    protected void Page_Load(object sender, EventArgs e)
    {
      AddOnLoadScriptLines(
        "page.addControl( new RequiredCombo('cboApplication', 'Aplicaciones') );");

      if (!IsPostBack)
      {
        if (Request["application"] != null)
        {
          ListControls.SelectByValue(cboApplication, Request["application"]);
          cboApplication.Enabled = false;
        }

        InitializeAbm(new UserProvider(new UndoableTransaction(CurrentUser)),
          grdMaster, lnkNew, lnkCopy, lnkDelete, lnkSave,
          lnkCancel, lnkSearch, txtName, grdDetail, lnkAttrAdd);


        ListControls.Fill(cboApplication, new ApplicationProvider(new SimpleTransaction(CurrentUser)).List(null),
          "Name", "Id", "[Seleccione]");
      }
    }

    private void UsersPage_MasterItemLoaded(object sender, UaMasterItemEventArgs<User> e)
    {
      var u = e.Element;
      txtName.Text = u.UserName;
      grdDetail.DataSource = new GroupProvider(e.Transaction).List(u);
      grdDetail.DataBind();
    }


    private void UsersPage_MasterItemSaved(object sender, UaMasterItemEventArgs<User> e)
    {
      if (IdApplication > 0)
      {
        var usgs = new UserGroupCollection();
        var u = e.Element;

        foreach (var g in (GroupCollection) grdDetail.DataSource)
        {
          usgs.Add(new UserGroup(u, g));
        }

        var ugp = new UserGroupProvider(e.Transaction);
        ugp.Remove(u, new GroupProvider(e.Transaction).List(u));

        if (ugp.SetItems(usgs) == null)
        {
          throw new Exception("Error al grabar los elementos.");
        }
      }
    }


    protected void cboApplication_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        IdApplication = int.Parse(cboApplication.SelectedItem.Value);
        grdMaster.Visible = true;
      }
      catch (Exception)
      {
        IdApplication = 0;
        grdMaster.Visible = false;
      }
    }


    protected void lnkAttrAdd_Click(object sender, EventArgs e)
    {
      OpenDialog(string.Format(seleccionarURL, IdApplication), 100, 280);
    }


    private void UsersPage_ReturnedFromDialog(object sender, UaDialogReturnEventArgs e)
    {
      if (e.WasCalledFrom(string.Format(seleccionarURL, IdApplication)))
      {
        try
        {
          var id = Convert.ToInt32(e.ReturnValue["group"]);

          if (!IsNull.This(id))
          {
            var lst = grdDetail.DataSource as GroupCollection;

            if (lst == null)
            {
              lst = new GroupCollection();
            }

            var trans = new SimpleTransaction(CurrentUser);
            trans.IdApplication = IdApplication;
            var gfilter = new Group();
            gfilter.Id = id;
            var g = new GroupProvider(trans).GetItem(gfilter);

            if (lst.Contains(g))
            {
              throw new Exception("El elemento ya existe en la lista.");
            }

            lst.Add(g);

            grdDetail.DataSource = lst;
            grdDetail.DataBind();
          }
        }
        catch (Exception ex)
        {
          ShowError("La selección no es válida. " + ex.Message);
        }
      }
    }


    private void grdDetail_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
      var lst = (GroupCollection) grdDetail.DataSource;
      lst.RemoveAt(e.Item.DataSetIndex);
      grdDetail.DataSource = lst;
      grdDetail.DataBind();
    }


    private void UsersGroupsPage_ABMModeChanged(object sender, AbmModeChangeEventArgs e)
    {
      if (e.Mode != DataAbmTemplateMode.ModeNone)
      {
        txtName.ReadOnly = true;
        txtName.CssClass = "box_disabled";
      }
      else
      {
        txtName.ReadOnly = false;
        txtName.CssClass = "box";
      }
    }


    private void UsersGroupsPage_MasterItemBeforeSearch(object sender, UaMasterItemEventArgs<User> e)
    {
      var u = e.Element;
      if (txtName.Text != string.Empty)
      {
        u.UserName = txtName.Text;
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
          this.UsersGroupsPage_MasterItemBeforeSearch);
      this.MasterItemSaved +=
        new EventHandler<UaMasterItemEventArgs<User>>(this.UsersPage_MasterItemSaved);
      this.MasterItemLoaded +=
        new EventHandler<UaMasterItemEventArgs<User>>(this.UsersPage_MasterItemLoaded);
      this.AbmModeChanged += new EventHandler<AbmModeChangeEventArgs>(this.UsersGroupsPage_ABMModeChanged);
      this.ReturnedFromDialog += new EventHandler<UaDialogReturnEventArgs>(this.UsersPage_ReturnedFromDialog);
      this.grdDetail.DeleteCommand += new DataGridCommandEventHandler(this.grdDetail_DeleteCommand);
    }

    #endregion
  }
}