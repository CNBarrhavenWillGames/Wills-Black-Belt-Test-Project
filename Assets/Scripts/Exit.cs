using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Exit : MonoBehaviour
{
    [SerializeField] private BackpackManager backpackManager;
    [SerializeField] private ObjectManager objectManager;


    private void OnCollisionEnter(Collision collision) // Handles Finishing the Day
    {
        if (collision.gameObject.tag == "Player")
        {
            int length = backpackManager.inventory.Count;
            for (int i = 0; i < objectManager.objectList.Count; i++)
            {
                DataStorage.saveData.activeObjects[i] = objectManager.objectList[i].activeSelf;
            }
            

            for (int i = 0; i < length; i++)
            {
                DataStorage.dayItemIDs.Add(backpackManager.inventory[i].GetComponent<InteractableStats>().id);
            }
            DataStorage.Save();
            SceneManager.LoadScene(2);
        }
    }
}
