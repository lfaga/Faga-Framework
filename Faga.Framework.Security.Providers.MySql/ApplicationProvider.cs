using System;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.MySql
{
  public class ApplicationProvider : BaseDAApplication
  {
    public override Application GetItem(int id)
    {
      return new Application(id, "Generic");
    }


    public override ApplicationCollection List(Application filter)
    {
      return new ApplicationCollection {GetItem(1)};
    }


    public override Application SetItem(Application app)
    {
      return app;
    }


    public override void Remove(int id)
    {
      throw new NotImplementedException();
    }
  }
}