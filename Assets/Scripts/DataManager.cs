using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // assume we have a vaslid data stirng
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        DataStorage.Save();
    }

    public void LoadData()
    {
        DataStorage.Load();
    }
}
