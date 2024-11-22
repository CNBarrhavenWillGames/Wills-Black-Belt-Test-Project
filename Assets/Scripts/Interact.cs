using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField] private GameObject interactObject;
    [SerializeField] private InteractableStats interactScript;

    [SerializeField] private MovementScript movementScript;
    [SerializeField] private BackpackManager backpackManager;

    [Header("Health Variables")]
    [SerializeField] private int damageStrength = 5;
    [SerializeField] private int damageRate = 30;
    private int damageTimer;
    private bool damaged;
    public int health = 100;
    public int totalHealth = 100;
    

    // Update is called once per frame
    private void Update()
    {
        if (damageTimer >= damageRate)
        {
            if (damaged)
            {
                health -= damageStrength;
                if (health <= 0)
                {
                    DataStorage.saveData.day = 0;
                    DataStorage.saveData.totalRadiance = 0;
                    DataStorage.saveData.totalFood = 0;
                    DataStorage.dayItemIDs = new List<string>();
                    DataStorage.Reset();
                    SceneManager.LoadScene(0);
                }
            }
            damageTimer = 0;
        }
        else
        {
            damageTimer++;
        }

        print(health);

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

        if (collider.gameObject.tag == "Damage")
        {
            damaged = true;
        }
    }

    private void OnTriggerExit(Collider other) // Get rid of the object data.
    {
        if (other.gameObject.tag == "Interactable")
        {
            interactObject = null;
        }

        if (other.gameObject.tag == "Damage")
        {
            damaged = false;
        }
    }
}
