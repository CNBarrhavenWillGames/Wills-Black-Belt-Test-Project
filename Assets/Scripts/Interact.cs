using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    [SerializeField] private List<GameObject> interactObjects;
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

    private void Start()
    {
        if (DataStorage.saveData.extraHealth == true)
        {
            totalHealth = 200;
            health = 200;
        }
        interactObjects = new List<GameObject>();
    }

    // Update is called once per frame
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.E) && interactObjects.Count > 0) // Picks up the item and grabs its data.
        {
            GameObject closestObject = null;
            float distance = float.PositiveInfinity;
            for (int i = 0; i < interactObjects.Count; i++)
            {
                if (Vector3.Distance(interactObjects[i].transform.position, gameObject.transform.position) <= distance)
                {
                    closestObject = interactObjects[i];
                    distance = Vector3.Distance(interactObjects[i].transform.position, gameObject.transform.position);
                }
            }
            GameObject interactObject = closestObject;

            interactScript = interactObject.GetComponent<InteractableStats>();

            if (interactScript.id == "health")
            {
                DataStorage.saveData.extraHealth = true;
            }

            if (movementScript.weight + interactScript.weight < MovementScript.maxWeight) 
            {
                movementScript.weight += interactScript.weight;
                DataStorage.dayRadiance += interactScript.radiance;
                DataStorage.dayFood += interactScript.food;
                backpackManager.AddSprite(interactObject);
                interactObjects.Remove(interactObject);
                interactObject = null;
                
            }
        }
    }


    private void FixedUpdate() 
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
                    DataStorage.lost = true;
                    DataStorage.loseReason = LoseReason.emptyHealth;
                    SceneManager.LoadScene(0);
                }
            }
            damageTimer = 0;
        }
        else
        {
            damageTimer++;
        }
    }
    private void OnTriggerEnter(Collider collider) // Get the object data.
    {
        if (collider.gameObject.tag == "Interactable")
        {
            interactObjects.Add(collider.gameObject);
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
            interactObjects.Remove(other.gameObject);
        }

        if (other.gameObject.tag == "Damage")
        {
            damaged = false;
        }
    }
}
