using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static readonly string SAVE_PATH = Path.Combine(Application.persistentDataPath, "save.json");
    
    private Saves _saveData;
    
    public Dictionary<string, string> AnalyticsEvents => _saveData.AnalyticsEvents;

    public SaveSystem()
    {
        _saveData = ReadFromFile();
    }

    public void Save()
    {
        if (TrySerializeProfileData(_saveData, out var profileDataJson))
        {
            WriteToFile(profileDataJson);
        }
    }

    public void AddEvent(string key, string value)
    {
        _saveData.AnalyticsEvents.Add(key, value);
    }

    public void ClearEvents()
    {
        _saveData.AnalyticsEvents.Clear();
    }
    
    private Saves ReadFromFile()
    {
        if (File.Exists(SAVE_PATH))
        {
            if (TryReadFile(SAVE_PATH, out var saveData))
            {
                return saveData;
            }
        }

        return new Saves();
    }
    
    private bool TryReadFile(string profileFilePath, out Saves saveData)
    {
        var profileDataJson = File.ReadAllText(profileFilePath);

        if (string.IsNullOrEmpty(profileDataJson))
        {
            saveData = null;
            return false;
        }

        return JSONSerializeHelper.TryDeserializeFromJson(profileDataJson, out saveData);
    }

    private void WriteToFile(string profileDataJson)
    {
        if (!Directory.Exists(Application.persistentDataPath))
            Directory.CreateDirectory(Application.persistentDataPath);

        try
        {
            File.WriteAllText(SAVE_PATH, profileDataJson);
        }
        catch (Exception e)
        {
            Debug.LogError($"Main profile write exception: {e}");
        }
    }
    
    private bool TrySerializeProfileData(Saves profileData, out string json)
    {
        try
        {
            json = JSONSerializeHelper.SerializeToJson(profileData);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError( $"Serializing profile error: {e}");
        }

        json = null;
        return false;
    }
}
