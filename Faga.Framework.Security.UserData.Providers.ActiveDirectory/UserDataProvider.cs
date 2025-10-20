using System;
using System.DirectoryServices;
using System.Globalization;
using Faga.Framework.Configuration;
using Faga.Framework.Security.Exceptions;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.UserData.Providers.ActiveDirectory
{
  public class UserDataProvider : IUserDataProvider, IDisposable
  {
    private readonly string _adPassword;
    private readonly string _adUser;

    private readonly DirectoryEntry _directoryEntry;
    private bool _disposed;


    public UserDataProvider()
    {
      var adPath = ApplicationConfiguration.UserDataProvider.ActiveDirectoryPath;
      _adUser = ApplicationConfiguration.UserDataProvider.ActiveDirectoryUser;
      _adPassword = ApplicationConfiguration.UserDataProvider.ActiveDirectoryPassword;

      _directoryEntry = new DirectoryEntry(adPath, _adUser,
        _adPassword, AuthenticationTypes.Secure);
    }


    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    public User FillUserData(User user)
    {
      if (user != null)
      {
        try
        {
          var n = user.UserName.Split('\\');
          var w = string.Format("{0}@{1}.com.ar", n[1], n[0]);

          var de = DeGetUser(w.ToLower(CultureInfo.InvariantCulture));

          if (de.Properties.Contains("mail"))
          {
            user.Email = de.Properties["mail"].Value.ToString();
          }

          if (de.Properties.Contains("givenName"))
          {
            user.Nombre = de.Properties["givenName"].Value.ToString();
          }

          if (de.Properties.Contains("sn"))
          {
            user.Apellido = de.Properties["sn"].Value.ToString();
          }
        }
        catch (Exception ex)
        {
          throw new UserDataProviderException(ex);
        }
      }

      return user;
    }


    private DirectoryEntry DeGetUser(string userPrincipalName)
    {
      var deSearch = new DirectorySearcher(_directoryEntry)
      {
        Filter = string.Format("(&(objectClass=user)(userPrincipalName={0}))", userPrincipalName)
      };

      var results = deSearch.FindOne();

      if (results != null)
      {
        return new DirectoryEntry(results.Path, _adUser, _adPassword,
          AuthenticationTypes.Secure);
      }
      return null;
    }


    private void Dispose(bool disposing)
    {
      // Check to see if Dispose has already been called.
      if (!_disposed)
      {
        // If disposing equals true, dispose all managed 
        // and unmanaged resources.
        if (disposing)
        {
          // Dispose managed resources.
          _directoryEntry.Close();
          _directoryEntry.Dispose();
        }

        // Call the appropriate methods to clean up 
        // unmanaged resources here.
        // If disposing is false, 
        // only the following code is executed.
      }
      _disposed = true;
    }
  }
}