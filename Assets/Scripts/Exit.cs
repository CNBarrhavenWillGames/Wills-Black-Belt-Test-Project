using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) // Handles Finishing the Day
    {
        if (collision.gameObject.tag == "Player")
        {
            DataStorage.Save();
            SceneManager.LoadScene(2);
        }
    }
}
