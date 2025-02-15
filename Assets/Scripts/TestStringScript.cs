using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TestStringScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private MovementScript movementScript;
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
        weight = 8,
        items = 9,
        field = 10,
        salmon = 11,
        newDay = 12,
        finalFood = 13,
        bestRadiance = 14,
    }

    [SerializeField] private textType id;

    // Update is called once per frame
    private void Update()
    {
        switch (id) // Sets the display based off its ID.
        {
            case textType.day:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    text.text = "Day " + DataStorage.saveData.day;
                    return;
                }
                text.text = "Day " + DataStorage.saveData.day + " Results:";
                break;
            case textType.totalRadiance:
                text.text = "Total Radiance: " + DataStorage.saveData.totalRadiance + "/1000";
                break;
            case textType.dayRadiance:
                text.text = "Radiance Collected Today: " + DataStorage.dayRadiance;
                break;
            case textType.totalFood:
                if (DataStorage.saveData.ketchup)
                {
                    text.text = ((DataStorage.saveData.totalFood / 100f) + 1) + " - 1";
                }
                else
                {
                    text.text = ((DataStorage.saveData.totalFood / 100f) + 1) + " - 1";
                }
                break;
            case textType.dayFood:
                text.text = "Food Collected Today: " + (DataStorage.dayFood / 100f);
                break;
            case textType.loadGame:
                text.text = "Start from Day " + (DataStorage.saveData.day);
                break;
            case textType.totalFood2:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    text.text = "Excess Food: " + (DataStorage.saveData.totalFood / 100f);
                    return;
                }
                text.text = "Total Food Units: " + (DataStorage.saveData.totalFood / 100f);
                break;
            case textType.title:
                if (DataStorage.lost == true)
                {
                    switch (DataStorage.loseReason) {
                        case LoseReason.starvation:
                            text.text = "You starved!";
                            break;
                        case LoseReason.emptyHealth:
                            text.text = "You died!";
                            break;
                        case LoseReason.outOfTime:
                            text.text = "You ran out of time!";
                            break;
                    }
                }
                else
                {
                    text.text = "Infiltration";
                }
                
                break;
            case textType.weight:
                Color color = text.color;
                Color.RGBToHSV(color, out float h, out float s, out float v);
                h = (100 - movementScript.weight) / 360;
                text.color = Color.HSVToRGB(h, s, v, false);
                text.text = "Weight: " + movementScript.weight + " (" + (100 - movementScript.weight)+ "% Movement Speed)";
                break;
            case textType.items:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    text.text = "Total Items Collected: " + DataStorage.saveData.totalItems;
                    return;
                }
                text.text = "Items Collected: " + DataStorage.dayItemIDs.Count + " (Total: " + DataStorage.saveData.totalItems + ")";
                break;
            case textType.field:
                if (DataStorage.saveData.totalRadiance >= 2000)
                {
                    text.text = "There is no more Radiance left in the field.";
                }
                text.text = "There is " + (2000 - DataStorage.saveData.totalRadiance) + " Radiance left in the field.";
                break;
            case textType.salmon:
                if (DataStorage.saveData.salmonEaten > 0)
                {
                    text.text = "Salmon Left: " + (DataStorage.saveData.salmonEaten * 0.25) + " Food Units";
                }
                else
                {
                    text.text = "";
                }
                break;
            /////
            /*
            ///
            |\---/|
            | o_o |
             \_^_/
            */
            //AMANDA WAS HERE!!!!!!!!!!!!!!!!!!!!!
            ////
            case textType.finalFood:
                text.text = "Total Food Units Collected: " + (DataStorage.saveData.totalFood2 / 100f);
                break;
            case textType.bestRadiance:
                text.text = "High Score: " + DataStorage.saveData.bestRadiance + " Radiance";
                break;
        }
        
    }
}
