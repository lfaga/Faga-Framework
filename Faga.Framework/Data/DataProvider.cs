using System.Collections.Specialized;
using System.Configuration.Provider;

namespace Faga.Framework.Data
{
  public abstract class DataProvider : ProviderBase
  {
    private NameValueCollection parameters;


    public override string Name
    {
      get { return "DataProvider"; }
    }

    public override string Description
    {
      get { return "Faga.Framework.Data.DataProvider"; }
    }

    public override void Initialize(string name, NameValueCollection config)
    {
      parameters = config;
      base.Initialize(name, config);
    }


    public IConnection GetSimpleConnection()
    {
      return GetSimpleConnection(parameters["DefaultConnectionString"]);
    }


    public abstract IConnection GetSimpleConnection(string connectionString);


    public IConnection GetCommitableConnection()
    {
      return GetCommitableConnection(parameters["DefaultConnectionString"]);
    }


    public abstract IConnection GetCommitableConnection(string connectionString);
  }
}