using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Faga.Framework.Data.Generics
{
  public abstract class BaseDAMasterData<TEntity, TEntityCollection>
    : BaseDAData
    where TEntityCollection : Collection<TEntity>
  {
    public abstract TEntity GetItem(TEntity filter);

    public abstract TEntityCollection List(TEntity filter);

    public abstract TEntity SetItem(TEntity entity);

    public abstract TEntityCollection SetItems(TEntityCollection entities);


    public abstract TEntityCollection SetItems(TEntityCollection existingEntities,
      TEntityCollection newEntities,
      Comparer<TEntity> comparer);


    public abstract void Remove(TEntity filter);
  }
}