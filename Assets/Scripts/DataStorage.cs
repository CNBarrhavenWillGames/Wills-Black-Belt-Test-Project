using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Text.Json;

public class SaveData
{
    public int day { get; set;  }
    public string[] totalItems { get; set; }
    public int totalRadiance { get; set; }
    public int totalFood { get; set; }

    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    public static SaveData FromJsonString(string jsonString)
    {
        return JsonUtility.FromJson<SaveData>(jsonString);
    }
}

public static class DataStorage
{

    public static SaveData saveData;

    public static int dayItems;
    public static int dayRadiance;
    public static int dayFood;

    public static void Save()
    {
        PlayerPrefs.SetString("saveData", saveData.ToJsonString());
    }

    public static void Load()
    {
        string dataString = PlayerPrefs.GetString("saveData");
        if (string.IsNullOrEmpty(dataString))
        {
            Debug.Log("data filed, making new one.");
            DataStorage.saveData = new SaveData();
        }
        else
        {
            Debug.Log("data succrss");
            DataStorage.saveData = SaveData.FromJsonString(dataString);
        }
    }

}
