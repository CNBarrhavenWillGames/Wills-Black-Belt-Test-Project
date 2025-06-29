using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayObjectScript : MonoBehaviour
{
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    [SerializeField] private float moveSpeed;

    private float minRotation = 60;
    private float maxRotation = 120;
    private float currentAngle = 60;
    private enum objectType
    {
        rotatingWall = 0,
        disappear = 1,
        lever = 2,
        leverRotation = 3,
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

    private void FixedUpdate()
    {
        switch (id) 
        {
            case objectType.lever:
                if (DataStorage.lever == true && transform.position.y < maxY)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + moveSpeed, transform.position.z);
                }
                else if (DataStorage.lever == false && transform.position.y > minY)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - moveSpeed, transform.position.z);
                }
                break;

            case objectType.leverRotation:
                if ((DataStorage.lever) && (currentAngle < maxRotation)) // If the lever is on and is still smaller than the max rotation,
                {
                    currentAngle += moveSpeed * Time.fixedDeltaTime;
                    transform.localRotation = Quaternion.Euler(currentAngle, 180, 0);
                    //transform.Rotate(moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else if ((!DataStorage.lever) && (currentAngle > minRotation)) // If the lever is off and it still greater the min rotation,
                {
                    currentAngle -= moveSpeed * Time.fixedDeltaTime;
                    transform.localRotation = Quaternion.Euler(currentAngle, 180, 0);
                    //transform.Rotate(-moveSpeed * Time.fixedDeltaTime, 0, 0);
                }
                print((transform.localEulerAngles.x + 360) % 360);
                break;
        }

    }
}
