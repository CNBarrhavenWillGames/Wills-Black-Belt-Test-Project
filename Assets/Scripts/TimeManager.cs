using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float time;
    [SerializeField] private GameObject symbol;
    private RectTransform rect;
    private float start;
    private float end;
    [SerializeField] private GameObject endObject;

    [SerializeField] private GameObject counter;
    [SerializeField] private int lengthOfDay;
    private int countdownStart;

    [SerializeField] private GameObject gameLight;
    // Start is called before the first frame update
    void Start()
    {
        countdownStart = lengthOfDay - 60;
        counter.SetActive(false);
        rect = symbol.GetComponent<RectTransform>();
        start = rect.anchoredPosition.x;
        end = endObject.GetComponent<RectTransform>().anchoredPosition.x;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() // assume 60fps?
    {
        if (DataStorage.saveData.day == 1)
        {
            return;
        }

        time += Time.fixedDeltaTime; 

        float newX = Mathf.Lerp(start, end, time/lengthOfDay); // Each day is 4 minutes.
        Color color = gameLight.GetComponent<Light>().color;
        Color.RGBToHSV(color, out float h, out float s, out float v);
        s = time / lengthOfDay;
        gameLight.GetComponent<Light>().color = Color.HSVToRGB(h, s, v, false);

        if (time >= lengthOfDay)
        {
            DataStorage.saveData.day = 0; // You can get rid of
            DataStorage.saveData.totalRadiance = 0; // these lines but
            DataStorage.saveData.totalFood = 0; // i'm scared something will stop working if i do
            DataStorage.dayItemIDs = new List<string>();
            DataStorage.Reset();
            DataStorage.lost = true;
            DataStorage.loseReason = LoseReason.outOfTime;
            SceneManager.LoadScene(0);
        }
        if (time >= countdownStart) 
        {
            counter.SetActive(true);
            counter.GetComponent<TMP_Text>().text = Mathf.Floor(lengthOfDay - time).ToString(); 
        }
        symbol.GetComponent<RectTransform>().anchoredPosition = new Vector2(newX, rect.anchoredPosition.y);
    }
}
