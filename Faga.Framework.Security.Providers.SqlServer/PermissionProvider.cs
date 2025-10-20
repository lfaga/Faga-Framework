using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class PermissionProvider : BaseDAPermission
  {
    public PermissionProvider()
    {
    }


    internal PermissionProvider(IConnection cn)
    {
      Connection = cn;
    }


    public override Permission GetItem(int id, int idApplication)
    {
      Permission ci = null;
      var pars = new ParameterCollection
      {
        new SqlParameter("id", id),
        new SqlParameter("idApplication", idApplication)
      };

      var ds = Connection.ExecuteDataSet("spPermissionGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ci = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ci;
    }


    public override PermissionCollection List(Permission filter)
    {
      return List(filter, int.MinValue);
    }


    public override PermissionCollection List(int idGroup, int idApplication)
    {
      var p = new Permission {IdApplication = idApplication};

      return List(p, idGroup);
    }


    private PermissionCollection List(Permission filter, int idGroup)
    {
      var cs = new PermissionCollection();
      var pars = new ParameterCollection();

      if (!IsNull.This(filter.Id))
      {
        pars.Add(new SqlParameter("id", filter.Id));
      }

      if (!IsNull.This(filter.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", filter.IdApplication));
      }

      if (!IsNull.This(filter.Description))
      {
        pars.Add(new SqlParameter("description", filter.Description));
      }

      if (!IsNull.This(idGroup))
      {
        pars.Add(new SqlParameter("idGroup", idGroup));
      }

      var ds = Connection.ExecuteDataSet("spPermissionList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        cs.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return cs;
    }


    private Permission RowToObject(DataRow dr)
    {
      return new Permission(
        ConvertIt.ToInt32(dr["id"]),
        ConvertIt.ToInt32(dr["idApplication"]),
        ConvertIt.ToString(dr["description"])
        );
    }


    public override Permission SetItem(Permission permission)
    {
      var pars = new ParameterCollection();

      if (!IsNull.This(permission.Id))
      {
        pars.Add(new SqlParameter("id", permission.Id));
      }

      if (!IsNull.This(permission.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", permission.IdApplication));
      }

      if (!IsNull.This(permission.Description))
      {
        pars.Add(new SqlParameter("description", permission.Description));
      }

      var ds = Connection.ExecuteDataSet("spPermissionSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    public override void Remove(int id, int idApplication)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("id", id),
        new SqlParameter("idApplication", idApplication)
      };


      Connection.ExecuteNonQuery("spPermissionRemove", pars);
    }
  }
}