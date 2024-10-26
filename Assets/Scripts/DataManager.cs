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
            dayCounter.text = "Day: " + DataStorage.saveData.day;
            totalRadianceCounter.text = "Total Radiance: " + DataStorage.saveData.totalRadiance;
            currentRadianceCounter.text = "Inventory Radiance: " + DataStorage.dayRadiance;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            DataStorage.saveData.totalRadiance += DataStorage.dayRadiance;
        }
    }
}
