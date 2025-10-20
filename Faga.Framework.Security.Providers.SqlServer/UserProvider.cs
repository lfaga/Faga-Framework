using System;
using System.Data;
using System.Data.SqlClient;
using Faga.Framework.Data;
using Faga.Framework.Security.Model;
using Faga.Framework.Security.Model.Collections;

namespace Faga.Framework.Security.Providers.SqlServer
{
  public class UserProvider : BaseDAUser
  {
    public UserProvider()
    {
    }


    internal UserProvider(IConnection cn)
    {
      Connection = cn;
    }


    public override User GetItem(int id)
    {
      User ui = null;
      var pars = new ParameterCollection {new SqlParameter("id", id)};


      var ds = Connection.ExecuteDataSet("spUserGetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        ui = RowToObject(ds.Tables[0].Rows[0]);
      }

      ds.Dispose();

      return ui;
    }


    public override UserCollection List(User filter)
    {
      return List(filter, int.MinValue, int.MinValue);
    }


    public override UserCollection List(User filter, int idGroup, int idApplication)
    {
      var cs = new UserCollection();
      var pars = new ParameterCollection();

      if (filter != null)
      {
        if (!IsNull.This(filter.Id))
        {
          pars.Add(new SqlParameter("id", filter.Id));
        }

        if (!IsNull.This(filter.UserName))
        {
          pars.Add(new SqlParameter("userName", filter.UserName));
        }

        if (!IsNull.This(filter.Nombre))
        {
          pars.Add(new SqlParameter("name", filter.Nombre));
        }

        if (!IsNull.This(filter.Apellido))
        {
          pars.Add(new SqlParameter("lastname", filter.Apellido));
        }

        if (!IsNull.This(filter.Telefono))
        {
          pars.Add(new SqlParameter("phone", filter.Telefono));
        }

        if (!IsNull.This(filter.Email))
        {
          pars.Add(new SqlParameter("email", filter.Email));
        }

        if (!IsNull.This(filter.IdLanguage))
        {
          pars.Add(new SqlParameter("idLanguage", filter.IdLanguage));
        }
      }

      if (!IsNull.This(idGroup))
      {
        pars.Add(new SqlParameter("idGroup", idGroup));
      }

      if (!IsNull.This(idApplication))
      {
        pars.Add(new SqlParameter("idApplication", idApplication));
      }

      var ds = Connection.ExecuteDataSet("spUserList", pars);

      var enu = ds.Tables[0].Rows.GetEnumerator();

      while (enu.MoveNext())
      {
        cs.Add(RowToObject((DataRow) enu.Current));
      }

      ds.Dispose();

      return cs;
    }


    public override User SetItem(User user)
    {
      var pars = new ParameterCollection();

      if (!IsNull.This(user.Id))
      {
        pars.Add(new SqlParameter("id", user.Id));
      }

      if (!IsNull.This(user.UserName))
      {
        pars.Add(new SqlParameter("userName", user.UserName));
      }

      if (!IsNull.This(user.Nombre))
      {
        pars.Add(new SqlParameter("name", user.Nombre));
      }

      if (!IsNull.This(user.Apellido))
      {
        pars.Add(new SqlParameter("lastname", user.Apellido));
      }

      if (!IsNull.This(user.Telefono))
      {
        pars.Add(new SqlParameter("phone", user.Telefono));
      }

      if (!IsNull.This(user.Email))
      {
        pars.Add(new SqlParameter("email", user.Email));
      }

      if (!IsNull.This(user.IdLanguage))
      {
        pars.Add(new SqlParameter("idLanguage", user.IdLanguage));
      }

      var ds = Connection.ExecuteDataSet("spUserSetItem", pars);

      if (ds.Tables[0].Rows.Count == 1)
      {
        return RowToObject(ds.Tables[0].Rows[0]);
      }

      return null;
    }


    public override void Remove(int id)
    {
      var pars = new ParameterCollection {new SqlParameter("id", id)};
      Connection.ExecuteNonQuery("spUserRemove", pars);
    }


    public override int Validate(string userName)
    {
      var pars = new ParameterCollection {new SqlParameter("userName", userName)};
      return ConvertIt.ToInt32(Connection.ExecuteScalar("spUserValidate", pars));
    }


    public override void SetOnline(int idUser, int idApplication,
      bool online)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idUser", idUser),
        new SqlParameter("idApplication", idApplication),
        new SqlParameter("online", online)
      };

      Connection.ExecuteNonQuery("spUserSetOnline", pars);
    }


    public override DateTime GetOnline(int idUser, int idApplication)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idUser", idUser),
        new SqlParameter("idApplication", idApplication)
      };

      return ConvertIt.ToDateTime(
        Connection.ExecuteScalar("spUserGetOnline", pars));
    }


    public override bool IsInRole(int idUser, int idGroup, int idApplication)
    {
      var pars = new ParameterCollection
      {
        new SqlParameter("idUser", idUser),
        new SqlParameter("idGroup", idGroup),
        new SqlParameter("idApplication", idApplication)
      };

      return ConvertIt.ToBoolean(Connection.ExecuteScalar("spUserIsInRole", pars));
    }


    private User RowToObject(DataRow dr)
    {
      return new User(
        ConvertIt.ToInt32(dr["id"]),
        ConvertIt.ToString(dr["userName"]),
        ConvertIt.ToString(dr["name"]),
        ConvertIt.ToString(dr["lastname"]),
        ConvertIt.ToString(dr["phone"]),
        ConvertIt.ToString(dr["email"]),
        ConvertIt.ToInt32(dr["idLanguage"])
        );
    }
  }
}