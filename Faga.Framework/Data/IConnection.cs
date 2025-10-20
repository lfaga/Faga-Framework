using System;
using System.Data;
using System.Data.Common;

namespace Faga.Framework.Data
{
  public interface IConnection : IDisposable
  {
    DbTransaction Transaction { get; }

    int CommandTimeout { get; set; }
    void Commit();

    void Rollback();

    int ExecuteNonQuery(string storeProcName, ParameterCollection parameters);

    int ExecuteNonQuery(string sqlText);

    DataSet ExecuteDataSet(string storeProcName, ParameterCollection parameters);

    DataSet ExecuteDataSet(string sqlText);

    object ExecuteScalar(string storeProcName, ParameterCollection parameters);

    object ExecuteScalar(string sqlText);

    DbParameter CreateParameter(string name, DbType type, object value);

    DbParameter CreateParameter(string name, object value);
  }
}