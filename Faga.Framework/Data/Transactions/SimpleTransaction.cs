using System;
using System.Security.Principal;
using Faga.Framework.Configuration;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Data.Transactions
{
  [Serializable]
  public class SimpleTransaction : BaseTransaction
  {
    public SimpleTransaction(IPrincipal user)
      : base(new User(user.Identity.Name), TransactionType.Simple,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SimpleTransaction(IPrincipal user, string connectionString)
      : base(new User(user.Identity.Name), TransactionType.Simple, connectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SimpleTransaction(IPrincipal user, string connectionString,
      int idApplication)
      : base(new User(user.Identity.Name), TransactionType.Simple, connectionString
        , idApplication)
    {
    }


    public SimpleTransaction(string userName)
      : base(new User(userName), TransactionType.Simple,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SimpleTransaction(string userName, string connectionString)
      : base(new User(userName), TransactionType.Simple, connectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SimpleTransaction(string userName, string connectionString,
      int idApplication)
      : base(new User(userName), TransactionType.Simple, connectionString, idApplication)
    {
    }


    public SimpleTransaction(User user)
      : base(user, TransactionType.Simple,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SimpleTransaction(User user, int idApplication)
      : base(user, TransactionType.Simple,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        idApplication)
    {
    }


    public SimpleTransaction(User user, string connectionString)
      : base(user, TransactionType.Simple, connectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public SimpleTransaction(User user, string connectionString,
      int idApplication)
      : base(user, TransactionType.Simple, connectionString, idApplication)
    {
    }
  }
}