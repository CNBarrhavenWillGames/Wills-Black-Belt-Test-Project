using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public TMP_Text dayCounter;
    public TMP_Text totalRadianceCounter;
    public TMP_Text currentRadianceCounter;
    public TMP_Text totalFoodCounter;
    public TMP_Text currentFoodCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        // assume we have a vaslid data stirng
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveData()
    {
        DataStorage.Save();
    }

    public void LoadData()
    {
        DataStorage.Load();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DataStorage.dayRadiance = 0;
            DataStorage.dayFood = 0;
            dayCounter.text = "Day: " + DataStorage.saveData.day;
            totalRadianceCounter.text = "Stashed Radiance: " + DataStorage.saveData.totalRadiance;
            currentRadianceCounter.text = "Inventory Radiance: " + DataStorage.dayRadiance;
            totalFoodCounter.text = "Stashed Food: " + (DataStorage.saveData.totalFood / 10);
            currentFoodCounter.text = "Inventory Food: " + DataStorage.dayFood;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            DataStorage.saveData.totalRadiance += DataStorage.dayRadiance;
            DataStorage.saveData.totalFood += DataStorage.dayFood;
        }
    }
}
