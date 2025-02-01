using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (DataStorage.saveData.paperclip)
        {
            Color color = gameObject.GetComponent<Renderer>().material.color;
            print("Previous Transparency: " + color.a);
            gameObject.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, color.a - 0.40f);
            print("Current Transparency: " + gameObject.GetComponent<Renderer>().material.color.a);

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
