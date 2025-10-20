using System.Reflection;
using Faga.Framework.Configuration;
using Faga.Framework.Data.Generics;

namespace Faga.Framework.Security.Providers
{
  public sealed class SecurityDAFactory
  {
    private SecurityDAFactory()
    {
    }


    public static BaseDAApplication DAApplication()
    {
      return (BaseDAApplication) LoadClass("ApplicationProvider");
    }


    public static BaseDAUser DAUser()
    {
      return (BaseDAUser) LoadClass("UserProvider");
    }


    public static BaseDAGroup DAGroup()
    {
      return (BaseDAGroup) LoadClass("GroupProvider");
    }


    public static BaseDAUserGroup DAUserGroup()
    {
      return (BaseDAUserGroup) LoadClass("UserGroupProvider");
    }


    public static BaseDAPermission DAPermission()
    {
      return (BaseDAPermission) LoadClass("PermissionProvider");
    }


    public static BaseDAGroupPermission DAGroupPermission()
    {
      return (BaseDAGroupPermission) LoadClass("GroupPermissionProvider");
    }


    public static BaseDAPermissionParam DAPermissionParam()
    {
      return (BaseDAPermissionParam) LoadClass("PermissionParamProvider");
    }


    public static BaseDAPermissionValue DAPermissionValue()
    {
      return (BaseDAPermissionValue) LoadClass("PermissionValueProvider");
    }


    private static BaseDAData LoadClass(string className)
    {
      var path = ApplicationConfiguration.SecurityProvider.Type;

      var provider = (BaseDAData) Assembly.Load(path).CreateInstance(
        string.Concat(path, ".", className));

      return provider;
    }
  }
}