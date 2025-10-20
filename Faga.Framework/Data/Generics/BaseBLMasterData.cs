using System.Collections.ObjectModel;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Data.Generics
{
  public abstract class BaseBLMasterData<TEntity, TEntityCollection>
    : BaseBLData
    where TEntityCollection : Collection<TEntity>
  {
    protected BaseBLMasterData(User user,
      BaseDAData daProvider)
      : base(user, daProvider)
    {
    }


    protected BaseBLMasterData(BaseTransaction trans,
      BaseDAData daProvider)
      : base(trans, daProvider)
    {
    }


    protected BaseDAMasterData<TEntity, TEntityCollection> MasterDataProvider
    {
      get { return (BaseDAMasterData<TEntity, TEntityCollection>) Provider; }
    }

    public abstract TEntity New();

    public abstract TEntity GetItem(TEntity filter);

    public abstract TEntityCollection List(TEntity filter);

    public abstract TEntity SetItem(TEntity entity);

    public abstract TEntityCollection SetItems(TEntityCollection entities);


    public abstract TEntityCollection SetItems(TEntityCollection existingEntities,
      TEntityCollection newEntities);


    public abstract void Remove(TEntity filter);
  }
}