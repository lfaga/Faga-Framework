using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Globalization;

namespace Faga.Framework.Data.OdbcClient
{
  public class BaseConnection : IDisposable
  {
    protected OdbcConnection cn;
    private bool disposed;


    protected BaseConnection()
    {
      CommandTimeout = 60;
    }


    public int CommandTimeout { get; set; }

    #region IDisposable Members

    public virtual void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

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
      var p = new OdbcParameter(name, value) {DbType = type};
      return p;
    }


    public DbParameter CreateParameter(string name, object value)
    {
      return new OdbcParameter(name, value);
    }


    private int ExecuteNonQuery(CommandType cmdType, string cmdText, ParameterCollection cmdParms)
    {
      OdbcCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);

      //VER SI cmd.Transaction Contiene la transaccion iniciada
      var val = cmd.ExecuteNonQuery();

      cmd.Dispose();
      return val;
    }


    private DataSet ExecuteDataSet(CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      OdbcCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);

      var da = new OdbcDataAdapter(cmd);
      var ds = new DataSet {Locale = CultureInfo.InvariantCulture};
      da.Fill(ds);

      da.Dispose();
      cmd.Dispose();

      return ds;
    }


    private object ExecuteScalar(CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      OdbcCommand cmd = null;

      PrepareCommand(ref cmd, cmdType, cmdText, cmdParms);
      var val = cmd.ExecuteScalar();

      cmd.Dispose();

      return val;
    }


    private void PrepareCommand(ref OdbcCommand cmd, CommandType cmdType,
      string cmdText, ParameterCollection cmdParms)
    {
      cmd = cn.CreateCommand();
      cmd.CommandType = cmdType;
      cmd.CommandTimeout = CommandTimeout;
      cmd.CommandText = cmdText;

      if (cmdParms != null)
      {
        foreach (DbParameter par in cmdParms)
        {
          cmd.Parameters.Add(par);
        }
      }
    }


    private void Dispose(bool disposing)
    {
      if (!disposed)
      {
        if (disposing)
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
        }
      }
      disposed = true;
    }
  }
}