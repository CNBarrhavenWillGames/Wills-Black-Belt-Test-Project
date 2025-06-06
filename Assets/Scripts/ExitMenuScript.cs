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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (safety == true)
            {
                DataStorage.lost = false;
                DataStorage.loseReason = LoseReason.none;
                SceneManager.LoadScene(0);
            }
            safety = true;
            text.text = "All progress from today will be lost. Are you sure?";
        }
        else
        {
            DataStorage.lost = false;
            DataStorage.loseReason = LoseReason.none;
            DataStorage.saveData.day++;
            DataStorage.Save();
            SceneManager.LoadScene(0);
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
