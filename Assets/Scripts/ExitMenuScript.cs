using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitMenuScript : MonoBehaviour
{
    private bool safety;
    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMenu()
    {
        if (safety == true)
        {
            SceneManager.LoadScene(0);
        }
        safety = true;
        text.text = "All progress from today will be lost. Are you sure?";
    }
}
