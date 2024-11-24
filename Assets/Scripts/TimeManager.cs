using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    private float time;
    [SerializeField] private GameObject symbol;
    private RectTransform rect;
    private float start;
    private float end;
    [SerializeField] private GameObject endObject;
    // Start is called before the first frame update
    void Start()
    {
        rect = symbol.GetComponent<RectTransform>();
        start = rect.position.x;
        end = endObject.GetComponent<RectTransform>().position.x;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        time += 1;
        float newX = Mathf.Lerp(start, end, time/18000); // Each day is 5 minutes.
        if (newX >= 18000)
        {
            DataStorage.saveData.day = 0;
            DataStorage.saveData.totalRadiance = 0;
            DataStorage.saveData.totalFood = 0;
            DataStorage.dayItemIDs = new List<string>();
            DataStorage.Reset();
            SceneManager.LoadScene(0);
        }
        symbol.GetComponent<RectTransform>().position = new Vector2(newX, rect.position.y);
    }
}
