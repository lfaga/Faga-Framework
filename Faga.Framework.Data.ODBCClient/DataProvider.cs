namespace Faga.Framework.Data.OdbcClient
{
  public class DataProvider : Data.DataProvider
  {
    public override IConnection GetSimpleConnection(string connectionString)
    {
      return Connection.Open(connectionString);
    }


    public override IConnection GetCommitableConnection(string connectionString)
    {
      return TransactedConnection.Open(connectionString);
    }
  }
}