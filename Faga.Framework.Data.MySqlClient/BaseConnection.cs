using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Faga.Framework.Data.MySqlClient
{
  public class BaseConnection : IDisposable
  {
    private bool _disposed;


    protected BaseConnection()
    {
      CommandTimeout = 60;
    }


    // Obsoleto
    // INFO:	MySQL currently does not support any method of canceling a pending or 
    //				exeucting operation. All commands issues against a MySQL server will
    //				execute until completion or exception occurs.
    public int CommandTimeout { get; set; }

    [CLSCompliant(false)]
    protected MySqlConnection Connection { get; set; }

    #region Public Methods

    public int ExecuteNonQuery(string storeProcName, ParameterCollection parameters)
    {
      return ExecuteNonQuery(CommandType.StoredProcedure, storeProcName, parameters);
    }


    public int ExecuteNonQuery(string sqlString)
    {
      return ExecuteNonQuery(CommandType.Text, sqlString, null);
    }


    public DataSet ExecuteDataSet(string storeProcName, ParameterCollection parameters)
    {
      return ExecuteDataSet(CommandType.StoredProcedure, storeProcName, parameters);
    }


    public DataSet ExecuteDataSet(string sqlString)
    {
      return ExecuteDataSet(CommandType.Text, sqlString, null);
    }


    public object ExecuteScalar(string storeProcName, ParameterCollection parameters)
    {
      return ExecuteScalar(CommandType.StoredProcedure, storeProcName, parameters);
    }


    public object ExecuteScalar(string sqlString)
    {
      return ExecuteScalar(CommandType.Text, sqlString, null);
    }


    public DbParameter CreateParameter(string name, DbType type, object value)
    {
      return new MySqlParameter(name, value) {DbType = type};
    }


    public DbParameter CreateParameter(string name, object value)
    {
      return new MySqlParameter(name, value);
    }

    #endregion

    #region Private Methods

    private int ExecuteNonQuery(CommandType cmdType, string cmdText,
      ParameterCollection cmdParms)
    {
      MySqlCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);

      //VER SI cmd.Transaction Contiene la transaccion iniciada
      var val = cmd.ExecuteNonQuery();

      cmd.Dispose();
      return val;
    }


    private DataSet ExecuteDataSet(CommandType cmdType, string cmdText,
      ParameterCollection cmdParms)
    {
      MySqlCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);

      var da = new MySqlDataAdapter(cmd);
      var ds = new DataSet {Locale = CultureInfo.InvariantCulture};
      da.Fill(ds);

      da.Dispose();
      cmd.Dispose();

      return ds;
    }


    private object ExecuteScalar(CommandType cmdType, string cmdText,
      ParameterCollection cmdParms)
    {
      MySqlCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);
      var val = cmd.ExecuteScalar();

      cmd.Dispose();

      return val;
    }


    private void PrepareCommand(ref MySqlCommand cmd, CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      cmd = Connection.CreateCommand();
      cmd.CommandType = cmdType;
      //cmd.CommandTimeout = CommandTimeout;
      cmd.CommandText = cmdText;

      if (cmdParms != null)
      {
        foreach (var par in cmdParms)
        {
          cmd.Parameters.Add(par);
        }
      }
    }

    #endregion

    #region IDisposable Members

    public virtual void Dispose()
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
          if (Connection != null)
          {
            if (Connection.State != ConnectionState.Closed)
            {
              Connection.Close();
            }
            Connection.Dispose();
            Connection = null;
          }
        }
      }
      _disposed = true;
    }

    #endregion
  }
}