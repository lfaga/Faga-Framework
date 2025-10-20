using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace Faga.Framework.Security
{
  /// <summary>
  ///   Summary description for UserImpersonation.
  /// </summary>
  public class UserImpersonation : IDisposable
  {
    private const int LOGON32_LOGON_INTERACTIVE = 2;
    private const int LOGON32_PROVIDER_DEFAULT = 0;


    private bool _disposed;
    private WindowsImpersonationContext impersonationContext;


    public UserImpersonation(string userName, string domain, string password)
    {
      if (!ImpersonateValidUser(userName, domain, password))
      {
        throw new ArgumentException(
          string.Format(CultureInfo.InvariantCulture,
            "Impersonation Failed. Cannot logon user {0}\\{1}",
            domain, userName));
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
    private static extern int LogonUser(
      string lpszUserName, string lpszDomain, string lpszPassword,
      int dwLogonType, int dwLogonProvider, ref IntPtr phToken);


    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int DuplicateToken(
      IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

    private bool ImpersonateValidUser(string userName, string domain, string password)
    {
      WindowsIdentity tempWindowsIdentity;
      var token = IntPtr.Zero;
      var tokenDuplicate = IntPtr.Zero;

      if (LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
        LOGON32_PROVIDER_DEFAULT, ref token) != 0)
      {
        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
        {
          tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
          impersonationContext = tempWindowsIdentity.Impersonate();
          if (impersonationContext != null)
          {
            return true;
          }
          return false;
        }
        return false;
      }
      return false;
    }


    private void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          impersonationContext.Undo();
        }
      }
      _disposed = true;
    }
  }
}