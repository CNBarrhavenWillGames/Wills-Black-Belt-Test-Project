using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private CameraRotation cameraScript;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Camera mainCamera;

    private enum sliderType
    {
        sensitivity = 0,
        volume = 1,
        invertX = 2,
        invertY = 3,
        option1 = 4,
        option2 = 5,
        option3 = 6,
        fov = 7,
    }

    [SerializeField] private sliderType type;
    // Start is called before the first frame update
    void Start()
    {
        switch (type)
        {
            case sliderType.sensitivity:
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Sensitivity");
                break;
            case sliderType.volume:
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");
                break;
            case sliderType.invertX:
                if (PlayerPrefs.GetInt("InvertX") == 0)
                {
                    gameObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                }
                break;
            case sliderType.invertY:
                if (PlayerPrefs.GetInt("InvertY") == 0)
                {
                    gameObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                }
                break;
            case sliderType.option1:
                if (PlayerPrefs.GetInt("Option1") == 0)
                {
                    gameObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                }
                break;
            case sliderType.option2:
                if (PlayerPrefs.GetInt("Option2") == 0)
                {
                    gameObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                }
                break;
            case sliderType.option3:
                if (PlayerPrefs.GetInt("Option3") == 0)
                {
                    gameObject.GetComponent<Toggle>().isOn = false;
                }
                else
                {
                    gameObject.GetComponent<Toggle>().isOn = true;
                }
                break;
            case sliderType.fov:
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat("FOV");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sensitivity()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            cameraScript.sensitivity = Mathf.RoundToInt(gameObject.GetComponent<Slider>().value);
        }
        PlayerPrefs.SetFloat("Sensitivity", Mathf.RoundToInt(gameObject.GetComponent<Slider>().value));
    }

    public void Volume()
    {
        audioMixer.SetFloat("Music Volume", gameObject.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("Volume", gameObject.GetComponent<Slider>().value);
    }

    public void InvertX()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("InvertX", 1);
        }
        else
        {
            PlayerPrefs.SetInt("InvertX", 0);
        }

    }

    public void InvertY()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("InvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("InvertY", 0);
        }

    }

    public void Option1()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("Option1", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Option1", 0);
        }

    }

    public void Option2()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("Option2", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Option2", 0);
        }

    }

    public void Option3()
    {
        if (gameObject.GetComponent<Toggle>().isOn)
        {
            PlayerPrefs.SetInt("Option3", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Option3", 0);
        }

    }

    public void FOV()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            mainCamera.fieldOfView = gameObject.GetComponent<Slider>().value;
        }
        PlayerPrefs.SetFloat("FOV", gameObject.GetComponent<Slider>().value);
    }
}
