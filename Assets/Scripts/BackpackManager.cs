using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UIElements;

public class BackpackManager : MonoBehaviour
{
    [SerializeField] private GameObject slot;

    [SerializeField] private int selectedSlot;

    public List<GameObject> inventory;
    [SerializeField] private List<GameObject> slots;

    [SerializeField] private MovementScript movementScript;

    [SerializeField] private Interact interactScript;

    [SerializeField] private TMP_Text selectedDisplay;

    // Update is called once per frame
    private void Update()
    {
        //print(activeSlotInt);
        selectedSlot += Mathf.RoundToInt(Input.mouseScrollDelta.y * -1);
        SetSlot();

        if (Input.GetKeyDown(KeyCode.Q) && inventory.Count > 0)
        {
           
            movementScript.weight -= inventory[selectedSlot].GetComponent<InteractableStats>().weight;
            DataStorage.dayRadiance -= inventory[selectedSlot].GetComponent<InteractableStats>().radiance;
            DataStorage.dayFood -= inventory[selectedSlot].GetComponent<InteractableStats>().food;

            DropItem();

            selectedSlot--;
        }
    }
    /// <summary>
    /// This function sets the active slot.
    /// </summary>
    private void SetSlot()
    {
        if (slots.Count == 0)
        {
            selectedDisplay.gameObject.SetActive(false);
            selectedSlot = 0;
            return;
        }
        else
        {
            selectedDisplay.gameObject.SetActive(true);
            selectedSlot = selectedSlot % slots.Count;
            
            if (selectedSlot < 0)
            {
                selectedSlot = slots.Count + selectedSlot;
            }

        }
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().color = Color.gray;
        }

        slots[selectedSlot].GetComponent<Image>().color = Color.white;

        GameObject selectedObject = inventory[selectedSlot];
        InteractableStats selectedStats = selectedObject.GetComponent<InteractableStats>();

        selectedDisplay.GetComponent<RectTransform>().anchoredPosition = new Vector2(slots[selectedSlot].transform.position.x - 2250, slots[selectedSlot].transform.position.y);
        selectedDisplay.text = selectedStats.weight + " Weight, " + selectedStats.radiance + " Radiance, " + (selectedStats.food / 100f) + " Food. " + selectedStats.property;
    }

    /// <summary>
    /// This function adds a new sprite to the player's inventory.
    /// </summary>
    public void AddSprite(GameObject item)
    {
        InteractableStats itemScript = item.GetComponent<InteractableStats>();

        inventory.Add(item);

        GameObject newSlot = Instantiate(slot, gameObject.transform);
        newSlot.GetComponent<Image>().sprite = itemScript.sprite;

        slots.Add(newSlot);

        item.SetActive(false);


    }

    /// <summary>
    /// This function removes the selected item from the player's inventory.
    /// </summary>
    private void DropItem()
    {

        interactScript.editProximityPrompt();

        GameObject destroyUISlot = slots[selectedSlot];
        Destroy(destroyUISlot);

        inventory[selectedSlot].SetActive(true);

        if (inventory[selectedSlot].GetComponent<InteractableStats>().id == "health")
        {
            DataStorage.saveData.extraHealth = false;
        }

        if (inventory[selectedSlot].GetComponent<InteractableStats>().id == "hermesBoots")
        {
            DataStorage.saveData.hermesBoots = false;
        }

        if (inventory[selectedSlot].GetComponent<InteractableStats>().id == "salmon")
        {
            DataStorage.saveData.salmonEaten = 0;
        }

        if (inventory[selectedSlot].GetComponent<InteractableStats>().id == "paperClip")
        {
            DataStorage.saveData.paperclip = false;
        }

        if (inventory[selectedSlot].GetComponent<InteractableStats>().id == "map")
        {
            DataStorage.saveData.map = false;
        }

        if (inventory[selectedSlot].GetComponent<InteractableStats>().id == "ketchup")
        {
            DataStorage.saveData.ketchup = false;
        }

        inventory.RemoveAt(selectedSlot);
        slots.RemoveAt(selectedSlot);
    }
}
