using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public GameObject interactObject;
    public InteractableStats interactScript;

    public MovementScript movementScript;
    public BackpackManager backpackManager;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactObject)
        {
            interactScript = interactObject.GetComponent<InteractableStats>();
            if (movementScript.weight + interactScript.weight < MovementScript.maxWeight) 
            {
                movementScript.weight += interactScript.weight;
                DataStorage.dayRadiance += interactScript.radiance;
                backpackManager.AddSprite(interactObject);
                interactObject = null;
            }
         

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Interactable")
        {
            interactObject = collider.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            interactObject = null;
        }
    }
}
