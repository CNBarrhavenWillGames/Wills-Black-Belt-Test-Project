using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BackpackManager : MonoBehaviour
{
    public GameObject activeSlot;

    public List<GameObject> inventory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSprite(GameObject item)
    {
        InteractableStats itemScript = item.GetComponent<InteractableStats>();

        switch (item.name)
        {
            case "sphere":
                inventory.Add(itemScript.prefab);


                activeSlot.SetActive(true);
                activeSlot.GetComponent<Image>().sprite = itemScript.sprite;

                Destroy(item);
                break;
        }
    }
}
