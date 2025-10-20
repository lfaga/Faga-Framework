using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class GroupPermissionProvider : BaseDAGroupPermission
  {
    public override GroupPermission GetItem(int idGroup, int idPermission,
      int idApplication)
    {
      var gp = new GroupPermission();

      var pars = new ParameterCollection
      {
        new SqlParameter("idGroup", idGroup),
        new SqlParameter("idPermission", idPermission),
        new SqlParameter("idApplication", idApplication)
      };

      var ds = Connection.ExecuteDataSet("spGroupPermissionGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        gp = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return gp;
    }


    public override GroupPermissionCollection List(GroupPermission filter)
    {
      var gps = new GroupPermissionCollection();
      var pars = new ParameterCollection();

      if ((filter.Group != null)
          && !IsNull.This(filter.Group.Id))
      {
        pars.Add(new SqlParameter("idGroup", filter.Group.Id));
      }

      if ((filter.Permission != null)
          && !IsNull.This(filter.Permission.Id))
      {
        pars.Add(new SqlParameter("idPermission", filter.Permission.Id));
      }

      if ((filter.Group != null)
          && !IsNull.This(filter.Group.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", filter.Group.IdApplication));
      }
      else if ((filter.Permission != null)
               && !IsNull.This(filter.Permission.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", filter.Permission.IdApplication));
      }


      var ds = Connection.ExecuteDataSet("spGroupPermissionList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        gps.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return gps;
    }


    public override GroupPermission SetItem(GroupPermission gp)
    {
      var pars = new ParameterCollection();

      if (!IsNull.This(gp.Group.Id))
      {
        pars.Add(new SqlParameter("idGroup", gp.Group.Id));
      }

      if (!IsNull.This(gp.Group.Id))
      {
        pars.Add(new SqlParameter("idPermission", gp.Permission.Id));
      }

      if (!IsNull.This(gp.Permission.Id))
      {
        pars.Add(new SqlParameter("idApplication", gp.Permission.IdApplication));
      }

      var ds = Connection.ExecuteDataSet("spGroupPermissionSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    public override void Remove(int idGroup, int idPermission, int idApplication)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idGroup", idGroup),
        new SqlParameter("idPermission", idPermission),
        new SqlParameter("idApplication", idApplication)
      };

      Connection.ExecuteNonQuery("spGroupPermissionRemove", pars);
    }


    private GroupPermission RowToObject(DataRow dr)
    {
      return new GroupPermission(new GroupProvider(Connection).GetItem(
        ConvertIt.ToInt32(dr["idGroup"]),
        ConvertIt.ToInt32(dr["idApplication"])),
        new PermissionProvider(Connection).GetItem(
          ConvertIt.ToInt32(dr["idPermission"]),
          ConvertIt.ToInt32(dr["idApplication"])));
    }
  }
}