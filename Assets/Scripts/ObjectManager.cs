using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<GameObject> objectList;
    // Start is called before the first frame update
    private void Start()
    {
        if (objectList.Count == DataStorage.saveData.activeObjects.Count)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                objectList[i].SetActive(DataStorage.saveData.activeObjects[i]);
            }
        }
        else
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                DataStorage.saveData.activeObjects.Add(true);
            }
        }
    }
}
