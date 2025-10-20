using System.ComponentModel;
using System.Web.Services;
using Faga.Framework.Security;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;
using Faga.Framework.Security.Transactions;

namespace App_Code.WebServices
{
  [WebService(Description = "Servicio centralizador de seguridad para aplicaciones.",
    Namespace = "http://framework.irsa.com.ar/WebServices/Security/")]
  public class SecurityProvider : WebService
  {
    public SecurityProvider()
    {
      //CODEGEN: This call is required by the ASP.NET Web Services Designer
      InitializeComponent();
    }

    #region Component Designer generated code

    //Required by the Web Services Designer 
    private IContainer components = null;


    /// <summary>
    ///   Required method for Designer support - do not modify
    ///   the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
    }


    /// <summary>
    ///   Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #endregion

    #region UserProvider

    private UserProvider GetUserProvider(int idApplication)
    {
      var trans = new SecurityTransaction(User) {IdApplication = idApplication};
      return new UserProvider(trans);
    }


    [WebMethod]
    public User UserGetItem(int id, int idApplication)
    {
      var filter = new User {Id = id};
      return GetUserProvider(idApplication).GetItem(filter);
    }


    [WebMethod]
    public UserCollection UserList(User filter, int idApplication)
    {
      var g = new Group {IdApplication = idApplication};
      return GetUserProvider(idApplication).List(filter, g);
    }


    [WebMethod]
    public UserCollection UserListPerGroup(Group grupo)
    {
      return GetUserProvider(grupo.IdApplication).List(grupo);
    }


    [WebMethod]
    public UserCollection UserListPerPermission(Permission permission)
    {
      return GetUserProvider(permission.IdApplication).List(permission);
    }


    [WebMethod]
    public UserCollection UserListPerParameter(PermissionValue permissionvalue)
    {
      return GetUserProvider(permissionvalue.IdApplication).List(permissionvalue);
    }


    [WebMethod]
    public bool UserIsInRole(User user, Group grupo)
    {
      return GetUserProvider(grupo.IdApplication).IsInRole(user, grupo);
    }


    [WebMethod]
    public User UserLogon(int idApplication)
    {
      return GetUserProvider(idApplication).Logon();
    }


    [WebMethod]
    public void UserLogoff(int idApplication)
    {
      GetUserProvider(idApplication).Logoff();
    }


    [WebMethod]
    public bool UserCheckPermission(User user, Permission permission)
    {
      return GetUserProvider(permission.IdApplication).CheckPermission(user, permission);
    }


    [WebMethod]
    public bool UserCheckParameter(User user, Permission permission, KeyValueStringPair parameter)
    {
      return GetUserProvider(permission.IdApplication).CheckParameter(user, permission,
        parameter.ToKeyValuePair());
    }

    #endregion

    #region PermissionProvider

    private PermissionProvider GetPermissionProvider(int idApplication)
    {
      var trans = new SecurityTransaction(User) {IdApplication = idApplication};
      return new PermissionProvider(trans);
    }


    [WebMethod]
    public Permission PermissionGetItem(int id, int idApplication)
    {
      var filter = new Permission {Id = id};
      return GetPermissionProvider(idApplication).GetItem(filter);
    }


    [WebMethod]
    public PermissionCollection PermissionList(Permission filter)
    {
      return GetPermissionProvider(filter.IdApplication).List(filter);
    }


    [WebMethod]
    public PermissionCollection PermissionListPerGroup(Group grupo)
    {
      return GetPermissionProvider(grupo.IdApplication).List(grupo);
    }

    #endregion

    #region GroupProvider

    private GroupProvider GetGroupProvider(int idApplication)
    {
      var trans = new SecurityTransaction(User) {IdApplication = idApplication};
      return new GroupProvider(trans);
    }


    [WebMethod]
    public Group GroupGetItem(int id, int idApplication)
    {
      var filter = new Group {Id = id};
      return GetGroupProvider(idApplication).GetItem(filter);
    }


    [WebMethod]
    public GroupCollection GroupList(Group filter)
    {
      return GetGroupProvider(filter.IdApplication).List(filter);
    }


    [WebMethod]
    public GroupCollection GroupsListPerPermission(Permission permission)
    {
      return GetGroupProvider(permission.IdApplication).List(permission);
    }


    [WebMethod]
    public GroupCollection GroupsListPerUser(User user, int idApplication)
    {
      return GetGroupProvider(idApplication).List(user);
    }

    #endregion

    #region PermissionValueProvider

    private PermissionValueProvider GetPermissionValueProvider(int idApplication)
    {
      var trans = new SecurityTransaction(User) {IdApplication = idApplication};
      return new PermissionValueProvider(trans);
    }


    [WebMethod]
    public PermissionValueCollection List(PermissionValue filter)
    {
      return GetPermissionValueProvider(filter.IdApplication).List(filter);
    }

    #endregion
  }
}