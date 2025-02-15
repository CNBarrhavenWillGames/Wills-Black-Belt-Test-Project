using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]   // need this so it's allowed to be made to a json
public class SaveData
{
    public int day; // turns out in this language and environment having { get; set; } actually breaks the json, contrary to the forum
    [SerializeField] // need this to mark list as a serializable thing (bc list is more complicated than ints kinda) (I think)
    public int totalItems;
    public int totalRadiance;
    public int totalFood;
    [SerializeField]
    public List<bool> activeObjects;
    public bool extraHealth;
    public bool hermesBoots;
    public bool paperclip;
    public bool map;
    public bool ketchup;
    public int salmonEaten;
    public int totalFood2;
    public List<string> totalItemIDs;
    public bool won;
    public int bestRadiance;
    // Generic constructor. sets things to default values
    public SaveData()
    {
        day = 0;
        totalItems = 0;
        totalRadiance = 0;
        totalFood = 0;
        activeObjects = new List<bool>();
        extraHealth = false;
        hermesBoots = false;
        paperclip = false;
        map = false;
        ketchup = false;
        salmonEaten = 0;
        totalFood2 = 0;
        totalItemIDs = new List<string>();
        won = false;
        bestRadiance = 0;
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

public enum LoseReason
{
    none = 0,
    starvation = 1,
    emptyHealth = 2,
    outOfTime = 3,
}

public static class DataStorage
{

    public static SaveData saveData;

    public static int dayItems;
    public static int dayRadiance;
    public static int dayFood;

    public static List<string> dayItemIDs;

    public static bool lost = false; // could merge this and below


    public static LoseReason loseReason = LoseReason.none;

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
        int bestRadianceSave = saveData.bestRadiance; // any other persistent saved things can be added here (or maybe make an array?)

        saveData = new SaveData();

        saveData.bestRadiance = bestRadianceSave;

        dayItemIDs = new List<string>();
        dayRadiance = 0;
        dayFood = 0;
        dayItems = 0;

        PlayerPrefs.SetString("saveData", saveData.ToJsonString());
        Debug.Log("reset data: " + saveData.ToJsonString());
    }
}
