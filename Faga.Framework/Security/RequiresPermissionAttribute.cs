using System;
using System.Security.Principal;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Transactions;

namespace Faga.Framework.Security
{
  /// <summary>
  ///   Summary description for RequiresPermissionAttribute.
  /// </summary>
  [CLSCompliant(false)]
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class RequiresPermissionAttribute : Attribute
  {
    private readonly User _currentUser;
    private readonly int _idPermission;


    public RequiresPermissionAttribute(IPrincipal currentUser, int idPermission)
    {
      using (var st = new SecurityTransaction(currentUser))
      {
        using (var up = new UserProvider(st))
        {
          _currentUser = up.Logon();
          _idPermission = idPermission;
        }
      }
    }


    public RequiresPermissionAttribute(User currentUser, int idPermission)
    {
      _currentUser = currentUser;
      _idPermission = idPermission;
    }


    public User CurrentUser
    {
      get { return _currentUser; }
    }

    public int IdPermission
    {
      get { return _idPermission; }
    }
  }
}