using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class PermissionParamProvider : BaseDAPermissionParam
  {
    public override PermissionParam GetItem(int idPermission, int idApplication,
      string key)
    {
      PermissionParam ci = null;
      var pars = new ParameterCollection
      {
        new SqlParameter("idPermission", idPermission),
        new SqlParameter("idApplication", idApplication),
        new SqlParameter("key", key)
      };

      var ds = Connection.ExecuteDataSet("spPermissionParamGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ci = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ci;
    }


    public override PermissionParamCollection List(PermissionParam filter)
    {
      var cs = new PermissionParamCollection();
      var pars = new ParameterCollection
      {
        new SqlParameter("idPermission", filter.IdPermission),
        new SqlParameter("idApplication", filter.IdApplication)
      };

      var ds = Connection.ExecuteDataSet("spPermissionParamList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        cs.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return cs;
    }


    public override PermissionParam SetItem(PermissionParam parameter)
    {
      var pars = new ParameterCollection();

      if (!IsNull.This(parameter.IdPermission))
      {
        pars.Add(new SqlParameter("idPermission", parameter.IdPermission));
      }

      if (!IsNull.This(parameter.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", parameter.IdApplication));
      }

      if (!IsNull.This(parameter.Key))
      {
        pars.Add(new SqlParameter("key", parameter.Key));
      }

      var ds = Connection.ExecuteDataSet("spPermissionParamSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    public override void Remove(int idPermission, int idApplication, string key)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idPermission", idPermission),
        new SqlParameter("idApplication", idApplication),
        new SqlParameter("key", key)
      };

      Connection.ExecuteNonQuery("spPermissionParamRemove", pars);
    }


    private PermissionParam RowToObject(DataRow dr)
    {
      return new PermissionParam(
        ConvertIt.ToInt32(dr["idPermission"]),
        ConvertIt.ToInt32(dr["idApplication"]),
        ConvertIt.ToString(dr["key"])
        );
    }
  }
}