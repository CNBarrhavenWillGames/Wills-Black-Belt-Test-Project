using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    [SerializeField] private AudioSource music;
    public bool menu;
    public OpenMenuScript openMenuScript;
    // Start is called before the first frame update

    private void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (menu)
        {
            if (openMenuScript.menuOpen)
            {
                paused = true;
            }
            else
            {
                paused = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = (!paused);
            }
        }

        
        if (paused)
        {

            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                // Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                music.Pause();
            }

            pauseMenu.SetActive(true);
            
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                // Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                music.UnPause();
            }
            
            pauseMenu.SetActive(false);
        }
        print(Cursor.lockState);
    }
}
