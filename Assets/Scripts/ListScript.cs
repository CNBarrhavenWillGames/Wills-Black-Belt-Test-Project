using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ListScript : MonoBehaviour
{
    [SerializeField] private GameObject[] spriteObjects;
    private int spriteNumber;

    public List<string> ids;
    public List<GameObject> things;
    public Dictionary<string, GameObject> spriteDictionary;

    // Start is called before the first frame update
    private void Start()
    {
        spriteDictionary = new Dictionary<string, GameObject>();
        int i = 0;
        spriteDictionary.Add("smallRadiance", spriteObjects[i++]);
        spriteDictionary.Add("largeRadiance", spriteObjects[i++]);
        spriteDictionary.Add("smallFood", spriteObjects[i++]);
        spriteDictionary.Add("health", spriteObjects[i++]);
        spriteDictionary.Add("tomato", spriteObjects[i++]);
        spriteDictionary.Add("melon", spriteObjects[i++]);
        spriteDictionary.Add("lollipop", spriteObjects[i++]);
        spriteDictionary.Add("candyCane", spriteObjects[i++]);
        spriteDictionary.Add("salmon", spriteObjects[i++]);
        spriteDictionary.Add("pizza", spriteObjects[i++]);
        spriteDictionary.Add("strawberry", spriteObjects[i++]);
        spriteDictionary.Add("chocolateCodey", spriteObjects[i++]);
        spriteDictionary.Add("magicCookie", spriteObjects[i++]);
        spriteDictionary.Add("ketchup", spriteObjects[i++]);
        spriteDictionary.Add("flask", spriteObjects[i++]);
        spriteDictionary.Add("jar", spriteObjects[i++]);
        spriteDictionary.Add("bucket", spriteObjects[i++]);
        spriteDictionary.Add("pyramid", spriteObjects[i++]);
        spriteDictionary.Add("sphere", spriteObjects[i++]);
        spriteDictionary.Add("cone", spriteObjects[i++]);
        spriteDictionary.Add("prism", spriteObjects[i++]);
        spriteDictionary.Add("star", spriteObjects[i++]);
        spriteDictionary.Add("diamond", spriteObjects[i++]);
        spriteDictionary.Add("paperClip", spriteObjects[i++]);
        spriteDictionary.Add("map", spriteObjects[i++]);
        spriteDictionary.Add("hermesBoots", spriteObjects[i++]);
        spriteDictionary.Add("goldenCodey", spriteObjects[i++]);
        spriteDictionary.Add("bodyArmour", spriteObjects[i++]);
        int length = DataStorage.dayItemIDs.Count;

        for (int j = 0; j < length; j++)
        {
            string selectedSprite = DataStorage.dayItemIDs[j];

            if (spriteDictionary.ContainsKey(selectedSprite))
            {
                Instantiate(spriteDictionary[selectedSprite], gameObject.transform);

            }
            else
            {
                print("Key not found: " + selectedSprite);
            }
        }
    }
}
