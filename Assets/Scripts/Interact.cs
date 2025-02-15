using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

            


            if (movementScript.weight + interactScript.weight < MovementScript.maxWeight) 
            {
                movementScript.weight += interactScript.weight;
                DataStorage.dayRadiance += interactScript.radiance;
                DataStorage.dayFood += interactScript.food;
                backpackManager.AddSprite(interactObject);
                interactObjects.Remove(interactObject);

                proximityPrompt.SetActive(false);
                editProximityPrompt();

                if (interactScript.id == "health")
                {
                    DataStorage.saveData.extraHealth = true;
                }

                if (interactScript.id == "hermesBoots")
                {
                    DataStorage.saveData.hermesBoots = true;
                }

                if (interactScript.id == "salmon")
                {
                    DataStorage.saveData.salmonEaten = 3;
                }

                if (interactScript.id == "paperClip")
                {
                    DataStorage.saveData.paperclip = true;

                }
                if (interactScript.id == "map")
                {
                    DataStorage.saveData.map = true;
                }
                if (interactScript.id == "ketchup")
                {
                    DataStorage.saveData.ketchup = true;
                }

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
        GameObject closestObject = checkDistance();


        if (closestObject == null)
        {
            proximityPrompt.SetActive(false);
            return;
        }

        proximityPrompt.SetActive(true);

        InteractableStats closestScript = closestObject.GetComponent<InteractableStats>();
        
        proximityPrompt.transform.GetChild(0).GetComponent<TMP_Text>().text = closestObject.name;
        proximityPrompt.transform.GetChild(1).GetComponent<TMP_Text>().text = "Weight: " + closestScript.weight;
        proximityPrompt.transform.GetChild(2).GetComponent<TMP_Text>().text = "Radiance " + closestScript.radiance.ToString();
        proximityPrompt.transform.GetChild(3).GetComponent<TMP_Text>().text = "Food: " + (closestScript.food / 100f).ToString();
        proximityPrompt.transform.GetChild(4).GetComponent<Image>().sprite = closestScript.sprite;
        proximityPrompt.transform.GetChild(6).GetComponent<TMP_Text>().text = closestScript.property;

        if (closestObject.GetComponent<InteractableStats>().weight >= (100 - movementScript.weight))
        {
            proximityPrompt.transform.GetChild(5).GetComponent<TMP_Text>().text = "Too Heavy";
            proximityPrompt.transform.GetChild(5).GetComponent<TMP_Text>().color = Color.red;
        }
        else
        {
            proximityPrompt.transform.GetChild(5).GetComponent<TMP_Text>().text = "E to Interact";
            proximityPrompt.transform.GetChild(5).GetComponent<TMP_Text>().color = Color.blue;
        }


    }
}
