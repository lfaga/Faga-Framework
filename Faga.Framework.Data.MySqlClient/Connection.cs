using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Faga.Framework.Data.MySqlClient
{
  public sealed class Connection : BaseConnection, IConnection
  {
    private Connection()
    {
    }


    public DbTransaction Transaction
    {
      get { return null; }
    }


    internal static IConnection Open(string connectionString)
    {
      var instance = new Connection();

      instance.Connection = new MySqlConnection(connectionString);
      instance.Connection.Open();

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