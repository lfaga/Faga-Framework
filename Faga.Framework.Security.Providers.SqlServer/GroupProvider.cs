using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class GroupProvider : BaseDAGroup
  {
    public GroupProvider()
    {
    }


    internal GroupProvider(IConnection cn)
    {
      Connection = cn;
    }


    public override Group GetItem(int id, int idApplication)
    {
      Group ci = null;
      var pars = new ParameterCollection
      {
        new SqlParameter("id", id),
        new SqlParameter("idApplication", idApplication)
      };


      var ds = Connection.ExecuteDataSet("spGroupGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ci = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ci;
    }


    public override GroupCollection List(Group filter)
    {
      return List(filter, int.MinValue, int.MinValue);
    }


    public override GroupCollection List(int idUser, int idPermission, int idApplication)
    {
      var g = new Group
      {
        IdApplication = idApplication
      };

      return List(g, idUser, idPermission);
    }


    public GroupCollection List(Group filter, int idUser, int idPermission)
    {
      var cs = new GroupCollection();
      var pars = new ParameterCollection();

      if (!IsNull.This(filter.Id))
      {
        pars.Add(new SqlParameter("id", filter.Id));
      }

      if (!IsNull.This(filter.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", filter.IdApplication));
      }

      if (!IsNull.This(filter.Name))
      {
        pars.Add(new SqlParameter("name", filter.Name));
      }

      if (!IsNull.This(filter.Description))
      {
        pars.Add(new SqlParameter("description", filter.Description));
      }

      if (!IsNull.This(idUser))
      {
        pars.Add(new SqlParameter("idUser", idUser));
      }

      if (!IsNull.This(idPermission))
      {
        pars.Add(new SqlParameter("idPermission", idPermission));
      }

      var ds = Connection.ExecuteDataSet("spGroupList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        cs.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return cs;
    }


    private Group RowToObject(DataRow dr)
    {
      return new Group(
        ConvertIt.ToInt32(dr["id"]),
        ConvertIt.ToInt32(dr["IdApplication"]),
        ConvertIt.ToString(dr["name"]),
        ConvertIt.ToString(dr["description"])
        );
    }


    public override Group SetItem(Group group)
    {
      var pars = new ParameterCollection();

      if (!IsNull.This(@group.Id))
      {
        pars.Add(new SqlParameter("id", @group.Id));
      }

      if (!IsNull.This(@group.IdApplication))
      {
        pars.Add(new SqlParameter("idApplication", @group.IdApplication));
      }

      if (!IsNull.This(@group.Name))
      {
        pars.Add(new SqlParameter("name", @group.Name));
      }

      if (!IsNull.This(@group.Description))
      {
        pars.Add(new SqlParameter("description", @group.Description));
      }

      var ds = Connection.ExecuteDataSet("spGroupSetItem", pars);

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


      Connection.ExecuteNonQuery("spGroupRemove", pars);
    }
  }
}