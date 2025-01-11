using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
                text.text = "Total Radiance: " + DataStorage.saveData.totalRadiance + "/100";
                break;
            case textType.dayRadiance:
                text.text = "Radiance Collected: " + DataStorage.dayRadiance;
                break;
            case textType.totalFood:
                text.text = ((DataStorage.saveData.totalFood / 100f) + 1) + " - 1";
                break;
            case textType.dayFood:
                text.text = "Food Collected: " + (DataStorage.dayFood / 100f);
                break;
            case textType.loadGame:
                text.text = "Start from Day " + (DataStorage.saveData.day);
                break;
            case textType.totalFood2:
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
                text.text = "Items Collected: " + DataStorage.dayItemIDs.Count + " (Total: " + DataStorage.saveData.totalItems + ")";
                break;
            case textType.field:
                text.text = "There is " + (350 - DataStorage.saveData.totalRadiance) + " radiance left in the field.";
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
        }
        
    }
}
