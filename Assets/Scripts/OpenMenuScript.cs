using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OpenMenuScript : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public bool menuOpen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMenu()
    {
        menuOpen = !menuOpen;
        if (menuOpen)
        {
            text.text = "Close Settings";
        }
        else
        {
            text.text = "Open Settiings";
        }

    }
}
