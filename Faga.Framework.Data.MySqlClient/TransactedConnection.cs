using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Faga.Framework.Data.MySqlClient
{
  public sealed class TransactedConnection : BaseConnection, IConnection
  {
    private bool _disposed;
    private MySqlTransaction _trans;


    private TransactedConnection()
    {
    }


    public DbTransaction Transaction
    {
      get { return _trans; }
    }


    internal static IConnection Open(string connectionString)
    {
      var instance = new TransactedConnection {Connection = new MySqlConnection(connectionString)};
      instance.Connection.Open();
      instance._trans = instance.Connection.BeginTransaction();

      return instance;
    }

    #region Private Methods

    private void FreeTransaction()
    {
      if (Connection != null)
      {
        if (Connection.State != ConnectionState.Closed)
        {
          Connection.Close();
        }
        Connection.Dispose();
        Connection = null;
      }
      if (_trans != null)
      {
        _trans = null;
      }
    }

    #endregion

    #region Public Methods

    public void Commit()
    {
      _trans.Commit();
      FreeTransaction();
    }


    public void Rollback()
    {
      _trans.Rollback();
      FreeTransaction();
    }


    public override void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }


    private void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          if (_trans != null)
          {
            _trans.Rollback();
          }
          FreeTransaction();
        }
      }
      _disposed = true;
    }

    #endregion
  }
}