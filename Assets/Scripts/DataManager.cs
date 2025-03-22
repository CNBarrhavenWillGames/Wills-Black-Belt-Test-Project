using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            DataStorage.dayItemIDs = new List<string>();
            DataStorage.lever = false;

            dayCounter.text = "Day: " + DataStorage.saveData.day;
            totalRadianceCounter.text = "Stashed Radiance: " + DataStorage.saveData.totalRadiance;
            currentRadianceCounter.text = "Inventory Radiance: " + DataStorage.dayRadiance;
            totalFoodCounter.text = "Stashed Food: " + (DataStorage.saveData.totalFood / 100f);
            totalFoodCounter.color = new Color(0.5f, 1f, 0.5f);
            if (DataStorage.saveData.totalFood < 100f)
            {
                print(DataStorage.saveData.totalFood);
                totalFoodCounter.color = new Color(1, 0.5f, 0.5f);
            }
            currentFoodCounter.text = "Inventory Food: " + DataStorage.dayFood;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            DataStorage.saveData.totalRadiance += DataStorage.dayRadiance;
            DataStorage.saveData.totalItems += DataStorage.dayItemIDs.Count;
            DataStorage.saveData.totalFood += DataStorage.dayFood;

            DataStorage.saveData.totalFood2 += DataStorage.dayFood;

            // dayItemIds have already been reset? totalItemIDs is most likely empty due to not printing
            for (int i = 0; i < DataStorage.dayItemIDs.Count; i++)
            {
                print("All Day Item IDs: " + DataStorage.dayItemIDs[i]);
                DataStorage.saveData.totalItemIDs.Add(DataStorage.dayItemIDs[i]);
            }
            DataStorage.saveData.totalItemIDs.Concat(DataStorage.dayItemIDs);
            for (int i = 0; i < DataStorage.saveData.totalItemIDs.Count; i++)
            {
                print("All Item IDs: " + DataStorage.saveData.totalItemIDs[i]);
            }
            print("Total Item IDs: " + DataStorage.saveData.totalItemIDs.Count);
           

            if (DataStorage.saveData.salmonEaten > 0)
            {
                DataStorage.saveData.totalFood += 25;
                DataStorage.saveData.salmonEaten -= 1;
            }


            if (DataStorage.saveData.ketchup)
            {
                DataStorage.saveData.totalFood *= 2;
                DataStorage.saveData.ketchup = false;
            }

            if (DataStorage.saveData.totalRadiance >= 1000 && DataStorage.saveData.won == false)
            {
                if (DataStorage.saveData.bestRadiance <= DataStorage.saveData.totalRadiance)
                {
                    DataStorage.saveData.bestRadiance = DataStorage.saveData.totalRadiance;
                }

                DataStorage.saveData.won = true;
                DataStorage.Save();
                SceneManager.LoadScene(3);
            }
            DataStorage.saveData.totalFood -= 100; // nom nom

            if (DataStorage.saveData.bestRadiance <= DataStorage.saveData.totalRadiance)
            {
                DataStorage.saveData.bestRadiance = DataStorage.saveData.totalRadiance;
            }

            if (DataStorage.saveData.totalFood < 0)
            {
                DataStorage.Reset();
                DataStorage.lost = true;
                DataStorage.loseReason = LoseReason.starvation;
                //DataStorage.saveData.day = 0;
                //DataStorage.saveData.totalRadiance = 0;
                //DataStorage.saveData.totalFood = 0;
                DataStorage.Save();
                SceneManager.LoadScene(0);
            }
        }
    }
}
