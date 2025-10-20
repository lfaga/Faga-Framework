using System.Configuration;
using System.IO;
using System.Xml;

namespace Faga.Framework.Configuration
{
  public class ApplicationConfigurationSection : ConfigurationSection
  {
    private ProviderSettingsCollection providers;
    private ReportingConfigurationElement reportingConfiguration;


    public ApplicationConfigurationSection()
    {
      providers = new ProviderSettingsCollection();
      reportingConfiguration = new ReportingConfigurationElement();
    }

    protected override bool OnDeserializeUnrecognizedAttribute(
      string name, string value)
    {
      return true;
    }


    public void Load(string filename)
    {
      var doc = new XmlDocument();
      doc.Load(filename);
      var xn = doc.GetElementsByTagName("ApplicationConfiguration")[0];
      using (var sr = new StringReader(xn.OuterXml))
      {
        using (var xr = XmlReader.Create(sr))
        {
          DeserializeSection(xr);
        }
      }
    }


    public void Save(string filename)
    {
      var s = SerializeSection(this, "ApplicationConfiguration",
        ConfigurationSaveMode.Full);
      using (var sw = new StreamWriter(filename, false))
      {
        sw.Write(s);
      }
    }

    #region Configuration Properties

    [ConfigurationProperty("ReportingConfiguration", IsRequired = false)]
    public ReportingConfigurationElement ReportingConfiguration
    {
      get
      {
        try
        {
          var s =
            (ReportingConfigurationElement) this["ReportingConfiguration"];
          reportingConfiguration = s;
        }
        catch
        {
        }
        return reportingConfiguration;
      }
    }


    [ConfigurationProperty("Providers", IsDefaultCollection = false)]
    [ConfigurationCollection(typeof (ProviderSettingsCollection),
      AddItemName = "AddProvider", ClearItemsName = "ClearProviders",
      RemoveItemName = "RemoveProvider")]
    public ProviderSettingsCollection Providers
    {
      get
      {
        try
        {
          var c =
            (ProviderSettingsCollection) base["Providers"];
          providers = c;
        }
        catch
        {
        }
        return providers;
      }
    }

    #endregion
  }
}