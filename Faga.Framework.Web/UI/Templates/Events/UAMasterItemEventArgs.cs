using System;
using Faga.Framework.Data.Transactions;

namespace Faga.Framework.Web.UI.Templates.Events
{
  public class UaMasterItemEventArgs<TEntity> : EventArgs
  {
    private readonly TEntity _element;
    private readonly BaseTransaction _transaction;


    public UaMasterItemEventArgs(TEntity element, BaseTransaction transaction)
    {
      _element = element;
      _transaction = transaction;
      MustSkip = false;
    }


    public TEntity Element
    {
      get { return _element; }
    }

    public BaseTransaction Transaction
    {
      get { return _transaction; }
    }


    internal bool MustSkip { get; private set; }

    public void SkipOperation()
    {
      MustSkip = true;
    }
  }
}