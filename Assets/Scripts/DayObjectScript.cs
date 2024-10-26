using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayObjectScript : MonoBehaviour
{
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        if (id == 1)
        {
            if (DataStorage.saveData.day % 2 == 0)
            {
                gameObject.transform.Rotate(0, 90, 0);
                print("Rotated");
            }

        }
        else if (id == 2) {

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
