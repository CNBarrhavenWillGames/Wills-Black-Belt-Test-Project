using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    public TMP_Text text;
    public enum counterType
    {
        currentRadiance = 0,
    }

    public counterType id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (id)
        {
            case counterType.currentRadiance:
                text.text = "Current Radiance: " + DataStorage.dayRadiance;
                break;
        }
    }
}
