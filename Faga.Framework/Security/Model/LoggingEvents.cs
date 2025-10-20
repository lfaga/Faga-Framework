namespace Faga.Framework.Security.Model
{
  /// <summary>
  ///   Summary description for LoggingEvents.
  /// </summary>
  internal class LoggingEvents
  {
    public static int ApplicationRemove = 12;
    public static int ApplicationSetItem = 11;

    public static int GroupPermissionRemove = 52;
    public static int GroupPermissionSetItem = 51;
    public static int GroupRemove = 32;
    public static int GroupSetItem = 31;

    public static int PermissionParamRemove = 72;
    public static int PermissionParamSetItem = 71;
    public static int PermissionRemove = 22;
    public static int PermissionSetItem = 21;
    public static int PermissionValueRemove = 62;
    public static int PermissionValueSetItem = 61;
    public static int UserCheckParameter = 4;
    public static int UserCheckPermission = 3;
    public static int UserGroupRemove = 42;
    public static int UserGroupSetItem = 41;
    public static int UserRemove = 2;
    public static int UserSetItem = 1;


    private LoggingEvents()
    {
    }
  }
}