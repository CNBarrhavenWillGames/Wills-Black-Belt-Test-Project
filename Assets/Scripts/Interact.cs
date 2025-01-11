using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Interact : MonoBehaviour
{
    [SerializeField] private List<GameObject> interactObjects;
    [SerializeField] private InteractableStats interactScript;

    [SerializeField] private MovementScript movementScript;
    [SerializeField] private BackpackManager backpackManager;
    [SerializeField] private GameObject proximityPrompt;

    [Header("Health Variables")]
    [SerializeField] private int damageStrength = 5;
    [SerializeField] private int damageStrength2 = 30;
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
        print("Nearby Objects:" + interactObjects.Count);

        if (Input.GetKeyDown(KeyCode.E) && interactObjects.Count > 0) // Picks up the item and grabs its data.
        {

            GameObject interactObject = checkDistance();

            interactScript = interactObject.GetComponent<InteractableStats>();

            if (interactScript.id == "health")
            {
                DataStorage.saveData.extraHealth = true;
            }

            if (interactScript.id == "salmon")
            {
                DataStorage.saveData.salmonEaten = 3;
            }


            if (movementScript.weight + interactScript.weight < MovementScript.maxWeight) 
            {
                movementScript.weight += interactScript.weight;
                DataStorage.dayRadiance += interactScript.radiance;
                DataStorage.dayFood += interactScript.food;
                backpackManager.AddSprite(interactObject);
                interactObjects.Remove(interactObject);

                editProximityPrompt();
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

            editProximityPrompt();

            proximityPrompt.SetActive(true);
        }

        if (collider.gameObject.tag == "Damage")
        {
            damaged = true;
        }
        if (collider.gameObject.tag == "Damage2")
        {
            health -= damageStrength2;
            collider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other) // Get rid of the object data.
    {


        if (other.gameObject.tag == "Interactable")
        {

            interactObjects.Remove(other.gameObject);

            if (interactObjects.Count <= 0)
            {
                proximityPrompt.SetActive(false);
            }
            else
            {
                editProximityPrompt();
            }

        }

        if (other.gameObject.tag == "Damage")
        {
            damaged = false;
        }
    }

    private GameObject checkDistance()
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
        return interactObject;
    }
    /// <summary>
    /// Edit the Proximity Prompt Data
    /// </summary>
    public void editProximityPrompt() 
    {
        proximityPrompt.transform.GetChild(0).GetComponent<TMP_Text>().text = checkDistance().gameObject.name;
        proximityPrompt.transform.GetChild(1).GetComponent<TMP_Text>().text = "Weight: " + checkDistance().GetComponent<InteractableStats>().weight;
        proximityPrompt.transform.GetChild(2).GetComponent<TMP_Text>().text = "Radiance " + checkDistance().GetComponent<InteractableStats>().radiance.ToString();
        proximityPrompt.transform.GetChild(3).GetComponent<TMP_Text>().text = "Food: " + (checkDistance().GetComponent<InteractableStats>().food / 100).ToString();
    }
}
