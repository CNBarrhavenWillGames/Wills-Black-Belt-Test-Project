using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class TestStringScript : MonoBehaviour
{
    public TMP_Text text;

    public enum textType
    {
        day = 0,
        totalRadiance = 1,
        dayRadiance = 2,
        totalFood = 3,
        dayFood = 4,
    }

    public textType id;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
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
