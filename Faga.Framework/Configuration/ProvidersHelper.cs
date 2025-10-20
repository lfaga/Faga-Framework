using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Reflection;

namespace Faga.Framework.Configuration
{
  public sealed class ProvidersHelper
  {
    private ProvidersHelper()
    {
    }


    public static ProviderBase InstantiateProvider(ProviderSettings providerSettings,
      Type providerType)
    {
      if (providerSettings == null)
      {
        throw new ArgumentNullException("providerSettings");
      }
      if (providerType == null)
      {
        throw new ArgumentNullException("providerType");
      }

      ProviderBase providerBase;

      var assemblyPath = providerSettings.Type != null
        ? providerSettings.Type.Trim()
        : null;

      if (string.IsNullOrEmpty(assemblyPath))
      {
        throw new ArgumentException("Provider.Type is not an assembly path.");
      }

      var assembly = Assembly.Load(assemblyPath);
      var typeName = string.Concat(assemblyPath, ".", providerSettings.Name);
      var type = assembly.GetType(typeName);

      if (type == null)
      {
        throw new ArgumentException(
          string.Format(CultureInfo.InvariantCulture,
            "Assembly {0} do not contains class {1}",
            assemblyPath, providerType.Name));
      }

      if (!providerType.IsAssignableFrom(type))
      {
        throw new ArgumentException(
          string.Format(CultureInfo.InvariantCulture,
            "Provider.Type must implement {0}", providerType.Name));
      }

      providerBase = (ProviderBase) assembly.CreateInstance(typeName);

      var parameters = providerSettings.Parameters;
      var parametersCopy =
        new NameValueCollection(parameters.Count, StringComparer.Ordinal);

      foreach (string key in parameters.Keys)
      {
        parametersCopy[key] = parameters[key];
      }

      try
      {
        providerBase.Initialize(providerSettings.Name, parametersCopy);
        return providerBase;
      }
      catch (Exception e)
      {
        if (e is ConfigurationException)
        {
          throw;
        }
        throw new ConfigurationErrorsException(e.Message,
          providerSettings.ElementInformation.Properties["type"].Source,
          providerSettings.ElementInformation.Properties["type"].LineNumber);
      }
    }
  }
}