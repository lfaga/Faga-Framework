using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAUserGroup : BaseDAData
  {
    public abstract UserGroup GetItem(int idUser, int idGroup, int idApplication);

    public abstract UserGroup SetItem(UserGroup userGroup);

    public abstract void Remove(int idUser, int idGroup, int idApplication);
  }
}