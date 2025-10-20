using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class ApplicationProvider : BaseDAApplication
  {
    public override Application GetItem(int id)
    {
      Application ui = null;
      var pars = new ParameterCollection {new SqlParameter("id", id)};

      var ds = Connection.ExecuteDataSet("spApplicationGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ui = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ui;
    }


    public override ApplicationCollection List(Application filter)
    {
      var cs = new ApplicationCollection();
      var pars = new ParameterCollection();

      if (filter != null)
      {
        if (!IsNull.This(filter.Id))
        {
          pars.Add(new SqlParameter("id", filter.Id));
        }

        if (!IsNull.This(filter.Name))
        {
          pars.Add(new SqlParameter("name", filter.Name));
        }
      }

      var ds = Connection.ExecuteDataSet("spApplicationList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        cs.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return cs;
    }


    public override Application SetItem(Application app)
    {
      var pars = new ParameterCollection();

      if (!IsNull.This(app.Id))
      {
        pars.Add(new SqlParameter("id", app.Id));
      }

      if (!IsNull.This(app.Name))
      {
        pars.Add(new SqlParameter("name", app.Name));
      }

      var ds = Connection.ExecuteDataSet("spApplicationSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    private Application RowToObject(DataRow dr)
    {
      return new Application(
        ConvertIt.ToInt32(dr["id"]),
        ConvertIt.ToString(dr["name"])
        );
    }


    public override void Remove(int id)
    {
      var pars = new ParameterCollection {new SqlParameter("id", id)};

      Connection.ExecuteNonQuery("spApplicationRemove", pars);
    }
  }
}