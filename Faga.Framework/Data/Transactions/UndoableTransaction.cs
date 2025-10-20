using System;
using System.Security.Principal;
using Faga.Framework.Configuration;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Data.Transactions
{
  [Serializable]
  public class UndoableTransaction : BaseTransaction
  {
    public UndoableTransaction(IPrincipal user)
      : base(new User(user.Identity.Name), TransactionType.Undoable,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public UndoableTransaction(IPrincipal user, string connectionString)
      : base(new User(user.Identity.Name), TransactionType.Undoable, connectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public UndoableTransaction(IPrincipal user, string connectionString,
      int idApplication)
      : base(new User(user.Identity.Name), TransactionType.Undoable, connectionString,
        idApplication)
    {
    }


    public UndoableTransaction(string userName)
      : base(new User(userName), TransactionType.Undoable,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public UndoableTransaction(string userName, string connectionString)
      : base(new User(userName), TransactionType.Undoable, connectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public UndoableTransaction(string userName, string connectionString,
      int idApplication)
      : base(new User(userName), TransactionType.Undoable, connectionString,
        idApplication)
    {
    }


    public UndoableTransaction(User user)
      : base(user, TransactionType.Undoable,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public UndoableTransaction(User user, int idApplication)
      : base(user, TransactionType.Undoable,
        ApplicationConfiguration.DataProvider.DefaultConnectionString,
        idApplication)
    {
    }


    public UndoableTransaction(User user, string connectionString)
      : base(user, TransactionType.Undoable, connectionString,
        ApplicationConfiguration.SecurityProvider.ApplicationId)
    {
    }


    public UndoableTransaction(User user, string connectionString,
      int idApplication)
      : base(user, TransactionType.Undoable, connectionString, idApplication)
    {
    }
  }
}