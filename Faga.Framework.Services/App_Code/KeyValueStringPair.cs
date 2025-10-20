using System.Collections.Generic;

/// <summary>
///   Summary description for KeyValueStringPair
/// </summary>
public struct KeyValueStringPair
{
  public string Key;
  public string Value;


  public KeyValueStringPair(string key, string value)
  {
    Key = key;
    Value = value;
  }

  public KeyValuePair<string, string> ToKeyValuePair()
  {
    return new KeyValuePair<string, string>(Key, Value);
  }
}