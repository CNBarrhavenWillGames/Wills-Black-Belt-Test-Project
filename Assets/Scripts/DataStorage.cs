using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using System.Text.Json;

[System.Serializable]   // need this so it's allowed to be made to a json
public class SaveData
{
    public int day; // turns out in this language and environement having { get; set; } actually breaks the json, contrary to the forum
    [SerializeField] // need this to mark list as a serializable thing (bc list is more complicated than ints kinda) (I think)
    public List<string> totalItems;
    public int totalRadiance;
    public int totalFood;

    // Generic constructor. sets things to default values
    public SaveData()
    {
        day = 0;
        totalItems = new List<string>();
        totalRadiance = 0;
        totalFood = 0;
    }

    // turn a saveData instance into a string
    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    // make a new saveData instance from a string
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

    public static List<string> dayItemIDs;

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
            Debug.Log("data success: " + dataString);
        }
    }

    public static void Reset()
    {
        saveData = new SaveData();
        PlayerPrefs.SetString("saveData", saveData.ToJsonString());
        Debug.Log("reset data: " + saveData.ToJsonString());
    }
}
