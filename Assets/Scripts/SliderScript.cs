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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            audioMixer.SetFloat("Music Volume", gameObject.GetComponent<Slider>().value);
        }
        PlayerPrefs.SetFloat("Volume", gameObject.GetComponent<Slider>().value);
    }
}
