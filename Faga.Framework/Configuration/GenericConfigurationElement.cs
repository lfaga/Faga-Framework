using System.Configuration;

namespace Faga.Framework.Configuration
{
  public class GenericConfigurationElement<T> : ConfigurationElement
  {
    public GenericConfigurationElement()
    {
    }


    public GenericConfigurationElement(T value)
    {
      Value = value;
    }

    #region Properties

    [ConfigurationProperty("Value", IsRequired = true, IsKey = true)]
    public T Value
    {
      get { return (T) this["Value"]; }
      set { this["Value"] = value; }
    }

    #endregion
  }
}