using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private enum counterType
    {
        currentRadiance = 0,
        currentFood = 1,
    }

    [SerializeField] private counterType id;

    // Update is called once per frame
    private void Update()
    {
        switch (id) // Sets the counter based on its ID.
        {
            case counterType.currentRadiance:
                text.text = "Current Radiance: " + DataStorage.dayRadiance;
                break;
            case counterType.currentFood:
                text.text = "Current Food: " + (DataStorage.dayFood / 10);
                break;
        }
    }
}
