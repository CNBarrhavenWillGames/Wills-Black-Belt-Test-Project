using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class BackpackManager : MonoBehaviour
{
    public GameObject activeSlot;

    public GameObject slot;

    public int activeSlotInt;

    public int selectedSlot;

    public List<GameObject> inventory;
    public List<GameObject> slots;

    public MovementScript movementScript; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //print(activeSlotInt);
        selectedSlot += Mathf.RoundToInt(Input.mouseScrollDelta.y * -1);
        setSlot();

        if (Input.GetKeyDown(KeyCode.Q) && inventory.Count > 0) 
        {
            movementScript.weight -= inventory[selectedSlot].GetComponent<InteractableStats>().weight;
            DataStorage.dayRadiance -= inventory[selectedSlot].GetComponent<InteractableStats>().radiance;
            DataStorage.dayFood -= inventory[selectedSlot].GetComponent<InteractableStats>().food;

            GameObject newSlot = Instantiate(slot, gameObject.transform);

            slots.Add(newSlot);

            inventory[selectedSlot].SetActive(true);
            inventory.RemoveAt(selectedSlot);
            slots[selectedSlot].SetActive(false);
            //activeSlotInt = inventory.Count;
           // activeSlot = slots[activeSlotInt];

            selectedSlot--;
            setSlot();

        }
    }

    public void setSlot()
    {
        if (activeSlotInt == 0)
        {
            selectedSlot = 0;
        }
        else
        {
            slots[selectedSlot].GetComponent<Image>().color = Color.gray;
            selectedSlot = selectedSlot % activeSlotInt;
            if (selectedSlot < 0)
            {
                selectedSlot = activeSlotInt + selectedSlot;
                slots[selectedSlot].GetComponent<Image>().color = Color.white;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].GetComponent<Image>().color = Color.gray;
        }
        
    }

    public void AddSprite(GameObject item)
    {
        InteractableStats itemScript = item.GetComponent<InteractableStats>();

        inventory.Add(itemScript.prefab);

       // activeSlot.SetActive(true);
       // activeSlot.GetComponent<Image>().sprite = itemScript.sprite;

       // activeSlotInt = inventory.Count;
      //  activeSlot = slots[activeSlotInt];

        item.SetActive(false);
    }
}
