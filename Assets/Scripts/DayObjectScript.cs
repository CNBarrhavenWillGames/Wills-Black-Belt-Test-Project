using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayObjectScript : MonoBehaviour
{
    private enum objectType
    {
        rotatingWall = 0,
        disappear = 1,
    }

    [SerializeField] private objectType id;

    // Start is called before the first frame update
    private void Start()
    {
        switch (id) // Sets the special object based off its ID.
        {
            case objectType.rotatingWall:
                if (DataStorage.saveData.day % 2 == 0)
                {
                    gameObject.transform.Rotate(0, 90, 0);
                }
                break;
            case objectType.disappear:
                if (DataStorage.saveData.day % 2 == 0) 
                {
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    gameObject.GetComponent<MeshRenderer>().material.color = new Color(gameObject.GetComponent<MeshRenderer>().material.color.r, gameObject.GetComponent<MeshRenderer>().material.color.g, gameObject.GetComponent<MeshRenderer>().material.color.b, 0.5f);
                }
                break;
        }
    }
}
