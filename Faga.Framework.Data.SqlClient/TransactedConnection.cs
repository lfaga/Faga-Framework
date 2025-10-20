using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Faga.Framework.Data.SqlClient
{
  public sealed class TransactedConnection : IConnection
  {
    private DbConnection _connection;
    private Database _database;


    private TransactedConnection()
    {
      CommandTimeout = 60;
    }


    public DbTransaction Transaction { get; private set; }

    public int CommandTimeout { get; set; }


    internal static IConnection Open(string connectionString)
    {
      var instance = new TransactedConnection();

      if (connectionString != null)
      {
        instance._database = DatabaseFactory.CreateDatabase(connectionString);
      }
      else
      {
        instance._database = DatabaseFactory.CreateDatabase();
      }
      instance._connection = instance._database.CreateConnection();
      instance._connection.Open();
      instance.Transaction = instance._connection.BeginTransaction();

      return instance;
    }

    #region Public Methods

    public void Commit()
    {
      Transaction.Commit();
      FreeTransaction();
    }


    public void Rollback()
    {
      Transaction.Rollback();
      FreeTransaction();
    }


    public int ExecuteNonQuery(string storeProcName, ParameterCollection parameters)
    {
      return ExecuteNonQuery(CommandType.StoredProcedure, storeProcName, parameters);
    }


    public int ExecuteNonQuery(string sqlText)
    {
      return ExecuteNonQuery(CommandType.Text, sqlText, null);
    }


    public DataSet ExecuteDataSet(string storeProcName, ParameterCollection parameters)
    {
      return ExecuteDataSet(CommandType.StoredProcedure, storeProcName, parameters);
    }


    public DataSet ExecuteDataSet(string sqlText)
    {
      return ExecuteDataSet(CommandType.Text, sqlText, null);
    }


    public object ExecuteScalar(string storeProcName, ParameterCollection parameters)
    {
      return ExecuteScalar(CommandType.StoredProcedure, storeProcName, parameters);
    }


    public object ExecuteScalar(string sqlText)
    {
      return ExecuteScalar(CommandType.Text, sqlText, null);
    }


    public DbParameter CreateParameter(string name, DbType type, object value)
    {
      return new SqlParameter(name, value) {DbType = type};
    }


    public DbParameter CreateParameter(string name, object value)
    {
      return new SqlParameter(name, value);
    }


    public void Dispose()
    {
      if (Transaction != null)
      {
        Transaction.Rollback();
      }
      FreeTransaction();
    }

    #endregion

    #region Private Methods

    private int ExecuteNonQuery(CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      DbCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);

      var val = _database.ExecuteNonQuery(cmd, Transaction);
      cmd.Dispose();

      return val;
    }


    private DataSet ExecuteDataSet(CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      DbCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);
      var ds = _database.ExecuteDataSet(cmd, Transaction);
      cmd.Dispose();
      return ds;
    }


    private object ExecuteScalar(CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      DbCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);
      var val = _database.ExecuteScalar(cmd, Transaction);

      cmd.Dispose();

      return val;
    }


    private void PrepareCommand(ref DbCommand cmd, CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      switch (cmdType)
      {
        case CommandType.Text:
          cmd = _database.GetSqlStringCommand(cmdText);
          break;

        case CommandType.StoredProcedure:
          cmd = _database.GetStoredProcCommand(cmdText);
          break;
      }

      if (cmdParms != null)
      {
        foreach (var parm in cmdParms)
        {
          _database.AddParameter(cmd, parm.ParameterName, parm.DbType, parm.Direction,
            parm.SourceColumn, parm.SourceVersion, parm.Value);
        }
      }

      cmd.CommandTimeout = CommandTimeout;
    }


    private void FreeTransaction()
    {
      if (_connection != null)
      {
        _connection.Close();
        _connection.Dispose();
        _connection = null;
      }
      if (Transaction != null)
      {
        Transaction.Dispose();
        Transaction = null;
      }
      _database = null;
    }

    #endregion
  }
}