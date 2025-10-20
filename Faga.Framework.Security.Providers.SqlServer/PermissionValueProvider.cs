using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class PermissionValueProvider : BaseDAPermissionValue
  {
    public override PermissionValue GetItem(int idPermission, int idApplication,
      string key, int idGroup, string value)
    {
      PermissionValue ci = null;
      var pars = new ParameterCollection
      {
        new SqlParameter("idPermission", idPermission),
        new SqlParameter("idApplication", idApplication),
        new SqlParameter("key", key),
        new SqlParameter("idGroup", idGroup),
        new SqlParameter("value", value)
      };

      var ds = Connection.ExecuteDataSet("spPermissionValueGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ci = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ci;
    }


    public override PermissionValueCollection List(PermissionValue filter)
    {
      var cs = new PermissionValueCollection();
      var pars = new ParameterCollection();

      if (!IsNull.This(filter.IdPermission))
      {
        pars.Add(new SqlParameter("idPermission", filter.IdPermission));
      }

      if (!IsNull.This(filter.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", filter.IdApplication));
      }

      if (!IsNull.This(filter.IdGroup))
      {
        pars.Add(new SqlParameter("idGroup", filter.IdGroup));
      }

      if (!IsNull.This(filter.Key))
      {
        pars.Add(new SqlParameter("key", filter.Key));
      }

      if (!IsNull.This(filter.Value))
      {
        pars.Add(new SqlParameter("value", filter.Value));
      }

      var ds = Connection.ExecuteDataSet("spPermissionValueList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        cs.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return cs;
    }


    private PermissionValue RowToObject(DataRow dr)
    {
      return new PermissionValue(
        ConvertIt.ToInt32(dr["idPermission"]),
        ConvertIt.ToInt32(dr["idApplication"]),
        ConvertIt.ToString(dr["key"]),
        ConvertIt.ToInt32(dr["idGroup"]),
        ConvertIt.ToString(dr["value"])
        );
    }


    public override PermissionValue SetItem(PermissionValue parameter)
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

      if (!IsNull.This(parameter.IdGroup))
      {
        pars.Add(new SqlParameter("idGroup", parameter.IdGroup));
      }

      if (!IsNull.This(parameter.Key))
      {
        pars.Add(new SqlParameter("key", parameter.Key));
      }

      if (!IsNull.This(parameter.Value))
      {
        pars.Add(new SqlParameter("value", parameter.Value));
      }

      var ds = Connection.ExecuteDataSet("spPermissionValueSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    public override void Remove(int idPermission, int idApplication,
      string key, int idGroup, string value)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idPermission", idPermission),
        new SqlParameter("idApplication", idApplication)
      };

      if (!IsNull.This(key))
      {
        pars.Add(new SqlParameter("key", key));
      }

      if (!IsNull.This(idGroup))
      {
        pars.Add(new SqlParameter("idGroup", idGroup));
      }

      if (!IsNull.This(value))
      {
        pars.Add(new SqlParameter("value", value));
      }

      Connection.ExecuteNonQuery("spPermissionValueRemove", pars);
    }
  }
}