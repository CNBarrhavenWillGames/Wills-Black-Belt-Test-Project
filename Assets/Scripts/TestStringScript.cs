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
                text.text = "Day: " + DataStorage.saveData.day;
                break;
            case textType.totalRadiance:
                text.text = "Total Radiance: " + DataStorage.saveData.totalRadiance;
                break;
            case textType.dayRadiance:
                text.text = "Radiance from Today: " + DataStorage.dayRadiance;
                break;
        }
        
    }
}
