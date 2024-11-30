using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TestStringScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private enum textType
    {
        day = 0,
        totalRadiance = 1,
        dayRadiance = 2,
        totalFood = 3,
        dayFood = 4,
        loadGame = 5,
        totalFood2 = 6,
        title = 7,
    }

    [SerializeField] private textType id;

    // Update is called once per frame
    private void Update()
    {
        switch (id) // Sets the display based off its ID.
        {
            case textType.day:
                text.text = "Day " + DataStorage.saveData.day + " Results:";
                break;
            case textType.totalRadiance:
                text.text = "Total Radiance: " + DataStorage.saveData.totalRadiance + "/1000";
                break;
            case textType.dayRadiance:
                text.text = "Radiance Collected: " + DataStorage.dayRadiance;
                break;
            case textType.totalFood:
                text.text = ((DataStorage.saveData.totalFood / 10) + 1) + " - 1";
                break;
            case textType.dayFood:
                text.text = "Food Collected: " + (DataStorage.dayFood / 10);
                break;
            case textType.loadGame:
                text.text = "Start from Day " + (DataStorage.saveData.day);
                break;
            case textType.totalFood2:
                text.text = "Total Food Units: " + (DataStorage.saveData.totalFood / 10);
                break;
            case textType.title:
                if (DataStorage.lost == true)
                {
                    text.text = "You lost!";
                }
                else
                {
                    text.text = "Welcome to the game.";
                }
                
                break;
        }
        
    }
}
