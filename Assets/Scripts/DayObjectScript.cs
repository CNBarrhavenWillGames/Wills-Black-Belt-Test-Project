using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayObjectScript : MonoBehaviour
{
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private float moveSpeed;
    private enum objectType
    {
        rotatingWall = 0,
        disappear = 1,
        lever = 2,
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

    private void Update()
    {
        switch (id) 
        {
            case objectType.lever:
                if (DataStorage.lever == true && transform.position.y <= maxY)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
                }
                else if (DataStorage.lever == false && transform.position.y >= minY)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
                }
                break;
        }

    }
}
