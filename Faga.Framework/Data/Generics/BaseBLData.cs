using System;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Data.Generics
{
  public class BaseBLData : IDisposable
  {
    private readonly BaseDAData provider;
    private bool disposed;
    private bool disposeTransaction;


    protected BaseBLData()
    {
      disposeTransaction = false;
    }


    protected BaseBLData(User user, BaseDAData provider)
    {
      disposeTransaction = true;
      Transaction = new SimpleTransaction(user);
      this.provider = provider;
      provider.Connection = Transaction.Connection;
    }


    protected BaseBLData(BaseTransaction trans, BaseDAData provider)
    {
      disposeTransaction = false;
      Transaction = trans;
      this.provider = provider;
      provider.Connection = Transaction.Connection;
    }


    protected BaseTransaction Transaction { get; private set; }

    protected BaseDAData Provider
    {
      get { return provider; }
    }

    #region IDisposable Members

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    #endregion

    public void ChangeTransaction(BaseTransaction trans)
    {
      if (disposeTransaction
          && (Transaction != null))
      {
        Transaction.Dispose();
      }
      disposeTransaction = false;
      Transaction = trans;
      provider.Connection = Transaction.Connection;
    }


    private void Dispose(bool disposing)
    {
      if (!disposed)
      {
        if (disposing)
        {
          if (disposeTransaction
              && (Transaction != null))
          {
            Transaction.Dispose();
            Transaction = null;
          }
        }
      }
      disposed = true;
    }
  }
}