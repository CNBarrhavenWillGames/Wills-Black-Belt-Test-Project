using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject interactObject;
    [SerializeField] private InteractableStats interactScript;

    [SerializeField] private MovementScript movementScript;
    [SerializeField] private BackpackManager backpackManager;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactObject) // Picks up the item and grabs its data.
        {
            interactScript = interactObject.GetComponent<InteractableStats>();
            if (movementScript.weight + interactScript.weight < MovementScript.maxWeight) 
            {
                movementScript.weight += interactScript.weight;
                DataStorage.dayRadiance += interactScript.radiance;
                DataStorage.dayFood += interactScript.food;
                backpackManager.AddSprite(interactObject);
                interactObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider collider) // Get the object data.
    {
        if (collider.gameObject.tag == "Interactable")
        {
            interactObject = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) // Get rid of the object data.
    {
        if (other.gameObject.tag == "Interactable")
        {
            interactObject = null;
        }
    }
}
