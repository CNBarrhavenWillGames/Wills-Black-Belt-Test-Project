using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private float totalWidth;
    public Interact interactScript;
    public GameObject healthBar;
    [SerializeField] private Rect rect;
    // Start is called before the first frame update
    void Start()
    {

        rect = healthBar.GetComponent<RectTransform>().rect;
        totalWidth = rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        rect.width = (interactScript.health / (float)interactScript.totalHealth) * totalWidth;
        //print("newWidth: ("+ interactScript.health + "/" + interactScript.totalHealth + ") *" + totalWidth);
        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(rect.width, rect.height);

        

        Color color = healthBar.GetComponent<Image>().color;
        Color.RGBToHSV(color, out float h, out float s, out float v);
        h = interactScript.health / 360f;
        //print(string.Format("{0}, gv vv  uyuy   {1}", h, s,v));

       // print("h:" + h + "s:" + s + "v:" + v);
        healthBar.GetComponent<Image>().color = Color.HSVToRGB(h, s, v, false);
    }

    bool DoTest(GameObject person, out float score)
    {
        score = 10.0f;
        return false; // "if, say, they got shot" - Dagan 2024
    }
}
