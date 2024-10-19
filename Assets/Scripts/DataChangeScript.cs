using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChangeScript : MonoBehaviour
{
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        if (type == 1)
        {
            Debug.Log("Added One Day");
            DataStorage.saveData.day += 1;
        }
        else if (type == 2) 
        {
            Debug.Log("Added Ten Radiance");
            DataStorage.saveData.totalRadiance += 10;
        }
    }
}
