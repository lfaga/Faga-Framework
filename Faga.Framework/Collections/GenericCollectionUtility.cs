using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Faga.Framework.Collections
{
  public class GenericCollectionUtility<TElement>
  {
    private readonly Collection<TElement> _col;


    public GenericCollectionUtility(Collection<TElement> collection)
    {
      _col = collection;
    }


    public GenericCollectionUtility(IList<TElement> collection)
    {
      _col = new Collection<TElement>(collection);
    }


    public GenericCollectionUtility(ICollection<TElement> collection)
    {
      _col = new Collection<TElement>();
      foreach (var element in collection)
      {
        _col.Add(element);
      }
    }


    public Collection<TElement> Collection
    {
      get { return _col; }
    }


    public bool Contains(TElement element, Comparer<TElement> comparer)
    {
      foreach (var item in Collection)
      {
        if (comparer.Compare(element, item) == 0)
        {
          return true;
        }
      }
      return false;
    }


    public void Remove(TElement element, Comparer<TElement> comparer)
    {
      var toremove = new Collection<TElement>();

      foreach (var item in Collection)
      {
        if (comparer.Compare(element, item) == 0)
        {
          toremove.Add(item);
        }
      }

      foreach (var item in toremove)
      {
        Collection.Remove(item);
      }
    }
  }
}