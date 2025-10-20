using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Reflection;
using Faga.Framework.Collections;

namespace Faga.Framework.Data.Generics
{
  public class DAGeneric<TEntity, TEntityCollection>
    : BaseDAMasterData<TEntity, TEntityCollection>
    where TEntity : new()
    where TEntityCollection : Collection<TEntity>, new()
  {
    private readonly string tableName;


    public DAGeneric()
    {
      var tia = (TableInfoAttribute[])
        typeof (TEntity).GetCustomAttributes(
          typeof (TableInfoAttribute), false);
      if (tia.Length == 1)
      {
        tableName = tia[0].Name;
      }
      else
      {
        tableName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(
          typeof (TEntity).Name);
      }
    }

    #region Public Methods

    public override TEntity GetItem(TEntity filter)
    {
      var entity = default(TEntity);
      var pars = GetKeyParams(filter, false);

      using (var ds = Connection.ExecuteDataSet(GetProcName("Select"), pars))
      {
        if (ds.Tables[0].Rows.Count == 1)
        {
          entity = RowToObject(ds.Tables[0].Rows[0]);
        }
      }

      return entity;
    }


    public override TEntityCollection List(TEntity filter)
    {
      var collection = new TEntityCollection();
      var pars = GetParams(filter, false);

      using (var ds = Connection.ExecuteDataSet(GetProcName("List"), pars))
      {
        foreach (DataRow row in ds.Tables[0].Rows)
        {
          collection.Add(RowToObject(row));
        }
      }

      return collection;
    }


    public override TEntity SetItem(TEntity entity)
    {
      TEntity res;

      if (GetItem(entity) == null)
      {
        res = InsertItem(entity);
      }
      else
      {
        res = UpdateItem(entity);
      }

      return res;
    }


    public virtual TEntity InsertItem(TEntity entity)
    {
      var res = default(TEntity);
      var pars = GetParams(entity, true);

      using (var ds = Connection.ExecuteDataSet(GetProcName("Insert"), pars))
      {
        if (ds.Tables[0].Rows.Count == 1)
        {
          res = RowToObject(ds.Tables[0].Rows[0]);
        }
      }

      return res;
    }


    public virtual TEntity UpdateItem(TEntity entity)
    {
      var res = default(TEntity);
      var pars = GetParams(entity, false);

      using (var ds = Connection.ExecuteDataSet(GetProcName("Update"), pars))
      {
        if (ds.Tables[0].Rows.Count == 1)
        {
          res = RowToObject(ds.Tables[0].Rows[0]);
        }
      }

      return res;
    }


    public override TEntityCollection SetItems(TEntityCollection entities)
    {
      var col = new TEntityCollection();

      foreach (var entity in entities)
      {
        col.Add(SetItem(entity));
      }

      return col;
    }


    public override TEntityCollection SetItems(TEntityCollection existingEntities,
      TEntityCollection newEntities,
      Comparer<TEntity> comparer)
    {
      var newCol = new TEntityCollection();

      var gcu
        = new GenericCollectionUtility<TEntity>(existingEntities);

      foreach (var newEntity in newEntities)
      {
        if (gcu.Contains(newEntity, comparer))
        {
          newCol.Add(UpdateItem(newEntity));
          gcu.Remove(newEntity, comparer);
        }
        else
        {
          newCol.Add(InsertItem(newEntity));
        }
      }

      foreach (var existingEntity in gcu.Collection)
      {
        Remove(existingEntity);
      }

      return newCol;
    }


    public override void Remove(TEntity filter)
    {
      var pars = GetParams(filter, false);
      if (pars.Count > 0)
      {
        Connection.ExecuteNonQuery(GetProcName("Delete"), pars);
      }
      else
      {
        throw new ArgumentException("Coudn't generate parameters.", "filter");
      }
    }


    public static TEntity RowToObject(DataRow dr)
    {
      object entity = new TEntity();

      foreach (var pi in typeof (TEntity).GetProperties())
      {
        var dagias =
          (DAGenericIgnoreAttribute[]) pi.GetCustomAttributes(
            typeof (DAGenericIgnoreAttribute), true);

        if (dagias.Length == 0)
        {
          pi.SetValue(entity, GetPropertyValue(dr, pi), null);
        }
      }

      return (TEntity) entity;
    }

    #endregion

    #region Private Methods

    private string GetProcName(string suffix)
    {
      return string.Format(CultureInfo.InvariantCulture, "sp{0}_{1}", tableName, suffix);
    }


    private static object GetPropertyValue(DataRow row, PropertyInfo pi)
    {
      if (row.IsNull(pi.Name))
      {
        return null;
      }
      if (row[pi.Name].GetType().Equals(typeof (sbyte))) //for MySql TinyInt (bool)
      {
        return (sbyte) row[pi.Name] != 0;
      }
      return row[pi.Name];
    }


    private ParameterCollection GetParams(TEntity filter, bool excludeAutoNumerics)
    {
      var pars = new ParameterCollection();

      foreach (var pi in typeof (TEntity).GetProperties())
      {
        if (!MustIgnore(pi)
            && !(IsAutoNumeric(pi) && excludeAutoNumerics))
        {
          var pname = string.Format(CultureInfo.InvariantCulture,
            "param_{0}", pi.Name);

          if (excludeAutoNumerics
              && MustCreateGuid(pi))
          {
            pi.SetValue(filter, Guid.NewGuid(), null);
          }

          if ((filter != null)
              && !IsNull.This(pi.GetValue(filter, null)))
          {
            pars.Add(Connection.CreateParameter(pname, pi.GetValue(filter, null)));
          }
          else
          {
            pars.Add(Connection.CreateParameter(pname, DBNull.Value));
          }
        }
      }

      return pars;
    }


    private ParameterCollection GetKeyParams(TEntity filter, bool excludeAutoNumerics)
    {
      var pars = new ParameterCollection();

      foreach (var pi in typeof (TEntity).GetProperties())
      {
        if (!MustIgnore(pi)
            && !(IsAutoNumeric(pi) && excludeAutoNumerics)
            && IsPrimaryKey(pi))
        {
          var pname = string.Format("param_{0}", pi.Name);

          if ((filter != null)
              && (pi.GetValue(filter, null) != null))
          {
            pars.Add(Connection.CreateParameter(pname, pi.GetValue(filter, null)));
          }
          else
          {
            pars.Add(Connection.CreateParameter(pname, DBNull.Value));
          }
        }
      }

      return pars;
    }


    private static bool MustIgnore(PropertyInfo pi)
    {
      return pi.GetCustomAttributes(typeof (DAGenericIgnoreAttribute), true).Length > 0;
    }


    private static bool MustCreateGuid(PropertyInfo pi)
    {
      return pi.GetCustomAttributes(typeof (AutoCreateGuidAttribute), true).Length > 0;
    }


    private static bool IsPrimaryKey(PropertyInfo pi)
    {
      return pi.GetCustomAttributes(typeof (PrimaryKeyAttribute), true).Length > 0;
    }


    private static bool IsAutoNumeric(PropertyInfo pi)
    {
      return pi.GetCustomAttributes(typeof (AutoNumericFieldAttribute), true).Length > 0;
    }

    #endregion
  }
}