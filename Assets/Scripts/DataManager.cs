using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [Header ("Counters")]
    [SerializeField] private TMP_Text dayCounter;
    [SerializeField] private TMP_Text totalRadianceCounter;
    [SerializeField] private TMP_Text currentRadianceCounter;
    [SerializeField] private TMP_Text totalFoodCounter;
    [SerializeField] private TMP_Text currentFoodCounter;
    
    // Start is called before the first frame update
    private void Awake()
    {
        LoadData();
    }

    private void SaveData()
    {
        DataStorage.Save();
    }

    /// <summary>
    /// This function loads the player's save data from DataStorage.
    /// </summary>
    private void LoadData()
    {
        DataStorage.Load();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DataStorage.dayRadiance = 0;
            DataStorage.dayFood = 0;
            DataStorage.dayItemIDs = new List<string>();

            dayCounter.text = "Day: " + DataStorage.saveData.day;
            totalRadianceCounter.text = "Stashed Radiance: " + DataStorage.saveData.totalRadiance;
            currentRadianceCounter.text = "Inventory Radiance: " + DataStorage.dayRadiance;
            totalFoodCounter.text = "Stashed Food: " + (DataStorage.saveData.totalFood / 10);
            currentFoodCounter.text = "Inventory Food: " + DataStorage.dayFood;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            DataStorage.saveData.totalRadiance += DataStorage.dayRadiance;
            DataStorage.saveData.totalFood += DataStorage.dayFood;
            DataStorage.saveData.totalFood -= 10;

            if (DataStorage.saveData.totalFood < 0)
            {
                DataStorage.saveData.day = 0;
                DataStorage.saveData.totalRadiance = 0;
                DataStorage.saveData.totalFood = 0;
                DataStorage.Save();
                SceneManager.LoadScene(0);
            }
        }
    }
}
