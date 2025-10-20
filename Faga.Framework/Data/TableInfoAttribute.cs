using System;

namespace Faga.Framework.Data
{
  [AttributeUsage(AttributeTargets.Class)]
  public class TableInfoAttribute : Attribute
  {
    public TableInfoAttribute(string _name)
    {
      Name = _name;
    }


    public string Name { get; set; }
  }
}