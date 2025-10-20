using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class UserGroupProvider : BaseDAUserGroup
  {
    public override UserGroup GetItem(int idUser, int idGroup, int idApplication)
    {
      UserGroup ug = null;
      var pars = new ParameterCollection
      {
        new SqlParameter("idUser", idUser),
        new SqlParameter("idGroup", idGroup),
        new SqlParameter("idApplication", idApplication)
      };

      var ds = Connection.ExecuteDataSet("spUserGroupGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ug = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ug;
    }


    public override UserGroup SetItem(UserGroup userGroup)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idUser", userGroup.User.Id),
        new SqlParameter("idGroup", userGroup.Group.Id),
        new SqlParameter("idApplication", userGroup.Group.IdApplication)
      };

      var ds = Connection.ExecuteDataSet("spUserGroupSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    public override void Remove(int idUser, int idGroup, int idApplication)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idUser", idUser),
        new SqlParameter("idGroup", idGroup),
        new SqlParameter("idApplication", idApplication)
      };

      Connection.ExecuteNonQuery("spUserGroupRemove", pars);
    }


    private UserGroup RowToObject(DataRow dr)
    {
      return new UserGroup(new UserProvider(Connection).GetItem(
        ConvertIt.ToInt32(dr["idUser"])),
        new GroupProvider(Connection).GetItem(
          ConvertIt.ToInt32(dr["idGroup"]),
          ConvertIt.ToInt32(dr["idApplication"])));
    }
  }
}