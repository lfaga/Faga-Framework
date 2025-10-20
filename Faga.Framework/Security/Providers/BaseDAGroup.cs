using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAGroup : BaseDAData
  {
    public abstract Group GetItem(int id, int idApplication);

    public abstract GroupCollection List(Group filter);

    public abstract GroupCollection List(int idUser, int idPermission, int idApplication);

    public abstract Group SetItem(Group group);

    public abstract void Remove(int id, int idApplication);
  }
}