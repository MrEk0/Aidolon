using System;
using UnityEngine;

public static class ServerUtilities
{
    public static byte[] Decoding(string data)
    {
        try
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"{typeof(ServerUtilities)}: {ex}");
            return new byte[0];
        }
    }
    
    public static string Encoding(byte[] data)
    {
        try
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"{typeof(ServerUtilities)}: {ex}");
            return string.Empty;
        }
    }
}
