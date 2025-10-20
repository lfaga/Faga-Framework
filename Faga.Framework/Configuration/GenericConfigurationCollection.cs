using System.Configuration;

namespace Faga.Framework.Configuration
{
  public class GenericConfigurationCollection<T> :
    ConfigurationElementCollection
  {
    public override ConfigurationElementCollectionType CollectionType
    {
      get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
    }

    public GenericConfigurationElement<T> this[int index]
    {
      get { return (GenericConfigurationElement<T>) BaseGet(index); }
      set
      {
        if (BaseGet(index) != null)
        {
          BaseRemoveAt(index);
        }
        BaseAdd(index, value);
      }
    }


    protected override ConfigurationElement CreateNewElement()
    {
      return new GenericConfigurationElement<T>();
    }


    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((GenericConfigurationElement<T>) element).Value;
    }


    public int IndexOf(GenericConfigurationElement<T> element)
    {
      return BaseIndexOf(element);
    }


    public void Add(T value)
    {
      BaseAdd(new GenericConfigurationElement<T>(value));
    }


    public void Add(GenericConfigurationElement<T> element)
    {
      BaseAdd(element);
    }


    protected override void BaseAdd(ConfigurationElement element)
    {
      BaseAdd(element, false);
    }


    public void Remove(GenericConfigurationElement<T> element)
    {
      if (BaseIndexOf(element) >= 0)
      {
        BaseRemove(element.Value);
      }
    }


    public void RemoveAt(int index)
    {
      BaseRemoveAt(index);
    }


    public void Clear()
    {
      BaseClear();
    }
  }
}