using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;

namespace Faga.Framework.Data.OdbcClient
{
  public sealed class TransactedConnection : BaseConnection, IConnection
  {
    private bool disposed;
    private OdbcTransaction trans;


    private TransactedConnection()
    {
    }

    #region IConnection Members

    public DbTransaction Transaction
    {
      get { return trans; }
    }

    #endregion

    #region Private Methods

    private void FreeTransaction()
    {
      if (cn != null)
      {
        if (cn.State != ConnectionState.Closed)
        {
          cn.Close();
        }
        cn.Dispose();
        cn = null;
      }
      if (trans != null)
      {
        trans = null;
      }
    }

    #endregion

    internal static IConnection Open(string connectionString)
    {
      var instance = new TransactedConnection();
      instance.cn = new OdbcConnection(connectionString);
      instance.cn.Open();
      instance.trans = instance.cn.BeginTransaction();

      return instance;
    }

    #region Public Methods

    public void Commit()
    {
      trans.Commit();
      FreeTransaction();
    }


    public void Rollback()
    {
      trans.Rollback();
      FreeTransaction();
    }


    public override void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    private void Dispose(bool disposing)
    {
      if (!disposed)
      {
        if (disposing)
        {
          if (trans != null)
          {
            trans.Rollback();
          }
          FreeTransaction();
        }
      }
      disposed = true;
    }

    #endregion
  }
}