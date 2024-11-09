using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayObjectScript : MonoBehaviour
{
    private enum objectType
    {
        rotatingWall = 0,
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
        }
    }
}
