using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalItemsScript : MonoBehaviour
{
    [SerializeField] private InteractableStats[] finalObjects;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DataStorage.saveData.totalItemIDs.Count; i++)
        {
            for (int j = 0; j < finalObjects.Length; j++)
            {
                InteractableStats objectgrr = finalObjects[j];
                string theIdOrSomething = objectgrr.id;
                string totalItemIdId = DataStorage.saveData.totalItemIDs[i];
                if (theIdOrSomething == totalItemIdId && objectgrr.gameObject.GetComponent<Rigidbody>().isKinematic == true)
                {
                    finalObjects[j].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    foreach (Collider collider in finalObjects[j].gameObject.GetComponents<Collider>())
                    {
                        collider.enabled = true;
                    }
                    break;
                }
            }

        }
    }
}
