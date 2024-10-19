using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Text.Json;

[System.Serializable]
public class SaveData
{
    public int day { get; set;  }
    public List<string> totalItems { get; set; }
    public int totalRadiance { get; set; }
    public int totalFood { get; set; }

    public SaveData()
    {
        day = 0;
        totalItems = new List<string>();
        totalRadiance = 0;
        totalFood = 0;
    }

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
        Debug.Log("saved data: " + saveData.ToJsonString());
        PlayerPrefs.SetString("saveData", saveData.ToJsonString());
    }

    public static void Load()
    {
        string dataString = PlayerPrefs.GetString("saveData");
        if (string.IsNullOrEmpty(dataString) || dataString == "{}")
        {
            DataStorage.saveData = new SaveData();
            Debug.Log("data filed, making new one.");
        }
        else
        {
            DataStorage.saveData = SaveData.FromJsonString(dataString);
            Debug.Log("data succrss: " + dataString);
        }
    }

}
