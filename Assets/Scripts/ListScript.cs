using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ListScript : MonoBehaviour
{
    [SerializeField] private GameObject smallRadianceSprite;
    [SerializeField] private GameObject largeRadianceSprite;
    [SerializeField] private GameObject smallFoodSprite;
    [SerializeField] private GameObject largeFoodSprite;
    // Start is called before the first frame update
    private void Start()
    {
        int length = DataStorage.dayItemIDs.Count;
        print(DataStorage.dayItemIDs.Count);

        for (int i = 0; i < length; i++)
        {
            string selectedSprite = DataStorage.dayItemIDs[i];

            switch (selectedSprite)
            {
                case "smallRadiance":
                    Instantiate(smallRadianceSprite, gameObject.transform);
                    break;
                case "largeRadiance":
                    Instantiate(largeRadianceSprite, gameObject.transform);
                    break;
                case "smallFood":
                    Instantiate(smallFoodSprite, gameObject.transform);
                    break;
                case "largeFood":
                    print("Large Food");
                    break;
            }
        }
    }
}
