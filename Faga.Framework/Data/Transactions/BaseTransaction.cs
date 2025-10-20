using System;
using Faga.Framework.Configuration;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Data.Transactions
{
  [Serializable]
  public class BaseTransaction : IDisposable
  {
    private bool disposed;


    protected BaseTransaction()
    {
    }


    protected BaseTransaction(User user, TransactionType type,
      string connectionString, int applicationId)
    {
      IdApplication = applicationId;
      Usuario = user;

      var dp =
        (DataProvider) ProvidersHelper.InstantiateProvider(
          ApplicationConfiguration.Providers["DataProvider"],
          typeof (DataProvider));


      if (type == TransactionType.Undoable)
      {
        Connection = dp.GetCommitableConnection(connectionString);
      }
      else
      {
        Connection = dp.GetSimpleConnection(connectionString);
      }

      Connection.CommandTimeout = ApplicationConfiguration.DataProvider.CommandTimeout;
    }

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    private void Dispose(bool disposing)
    {
      if (!disposed)
      {
        if (disposing)
        {
          if (Connection != null)
          {
            Connection.Dispose();
            Connection = null;
          }
        }
      }
      disposed = true;
    }

    #region Public Methods

    public virtual void Commit()
    {
      Connection.Commit();
    }


    public virtual void Rollback()
    {
      Connection.Rollback();
    }

    #endregion

    #region Public Properties

    public IConnection Connection { get; private set; }


    public User Usuario { get; set; }


    public int IdApplication { get; set; }

    #endregion
  }
}