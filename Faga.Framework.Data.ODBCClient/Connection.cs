using System.Data.Common;
using System.Data.Odbc;

namespace Faga.Framework.Data.OdbcClient
{
  public sealed class Connection : BaseConnection, IConnection
  {
    private Connection()
    {
    }

    #region IConnection Members

    public DbTransaction Transaction
    {
      get { return null; }
    }

    #endregion

    internal static IConnection Open(string connectionString)
    {
      var instance = new Connection {cn = new OdbcConnection(connectionString)};

      instance.cn.Open();

      return instance;
    }

    #region Public Methods

    public void Commit()
    {
    }


    public void Rollback()
    {
    }

    #endregion
  }
}