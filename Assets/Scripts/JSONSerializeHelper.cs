using System;
using UnityEngine;

public static class JSONSerializeHelper
{
    public static string SerializeToJson(object obj, bool prettyPrint = false)
    {
        try
        {
            return JsonUtility.ToJson(obj, prettyPrint);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            return string.Empty;
        }
    }
    
    public static bool TryDeserializeFromJson<T>(string json, out T result)
    {
        try
        {
            result = JsonUtility.FromJson<T>(json);
            return result != null;
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);

            result = default;
            return false;
        }
    }
}
