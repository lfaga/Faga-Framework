using System.Collections.Generic;
using System.Collections.ObjectModel;
using Faga.Framework.Data.Transactions;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Data.Generics
{
  public class BLGeneric<TEntity, TEntityCollection>
    : BaseBLMasterData<TEntity, TEntityCollection>
    where TEntity : new()
    where TEntityCollection : Collection<TEntity>, new()
  {
    public BLGeneric(User user)
      : base(user, new DAGeneric<TEntity, TEntityCollection>())
    {
    }


    public BLGeneric(BaseTransaction trans)
      : base(trans, new DAGeneric<TEntity, TEntityCollection>())
    {
    }


    private new DAGeneric<TEntity, TEntityCollection> Provider
    {
      get { return (DAGeneric<TEntity, TEntityCollection>) base.Provider; }
    }


    public override TEntity New()
    {
      return new TEntity();
    }


    public override TEntity GetItem(TEntity filter)
    {
      return Provider.GetItem(filter);
    }


    public override TEntityCollection List(TEntity filter)
    {
      return Provider.List(filter);
    }


    public override TEntity SetItem(TEntity entity)
    {
      return Provider.SetItem(entity);
    }


    public override TEntityCollection SetItems(TEntityCollection entities)
    {
      return Provider.SetItems(entities);
    }


    public virtual TEntityCollection SetItems(TEntityCollection existingEntities,
      TEntityCollection newEntities,
      Comparer<TEntity> comparer)
    {
      return Provider.SetItems(existingEntities, newEntities, comparer);
    }


    public override TEntityCollection SetItems(TEntityCollection existingEntities,
      TEntityCollection newEntities)
    {
      return Provider.SetItems(existingEntities, newEntities,
        new PrimaryKeyComparer<TEntity>());
    }


    public override void Remove(TEntity filter)
    {
      Provider.Remove(filter);
    }
  }
}