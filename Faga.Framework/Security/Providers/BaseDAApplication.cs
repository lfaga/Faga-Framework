using Faga.Framework.Data.Generics;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers
{
  public abstract class BaseDAApplication : BaseDAData
  {
    public abstract Application GetItem(int id);

    public abstract ApplicationCollection List(Application filter);

    public abstract Application SetItem(Application app);

    public abstract void Remove(int id);
  }
}