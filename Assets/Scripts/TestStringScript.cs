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
                text.text = "Total Food Units: " + (DataStorage.saveData.totalFood / 10);
                break;
            case textType.dayFood:
                text.text = "Food Collected: " + (DataStorage.dayFood / 10);
                break;
        }
        
    }
}
