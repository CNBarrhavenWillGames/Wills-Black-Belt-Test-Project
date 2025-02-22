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

    private enum sliderType
    {
        sensitivity = 0,
        volume = 1,
        invertX = 2,
        invertY = 3,
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
}
