using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataChangeScript : MonoBehaviour
{

    private enum buttonType
    {
        addOneDay = 1,
        addTenRadiance = 2,
        saveData = 3,
        resetData = 4,
        newGame = 5,
    }

    private void Start()
    {
        if (DataStorage.saveData.day == 0 && type == buttonType.saveData && SceneManager.GetActiveScene().buildIndex == 0)
        {
            gameObject.SetActive(false);
        }
    }

    [SerializeField] private buttonType type;

    public void Change()
    {
        switch (type) // Sets button type based off its ID.
        {
            case buttonType.addOneDay:
                Debug.Log("Added One Day");
                DataStorage.saveData.day += 1;
                break;
            case buttonType.addTenRadiance:
                Debug.Log("Added Ten Radiance");
                DataStorage.saveData.totalRadiance += 10;
                break;
            case buttonType.saveData:
                DataStorage.saveData.day += 1;
                DataStorage.Save();
                SceneManager.LoadScene(1);
                break;
            case buttonType.resetData:
                DataStorage.Reset();
                DataStorage.lost = true;
                DataStorage.dayRadiance = 0;
                DataStorage.dayFood = 0;
                DataStorage.dayItems = 0;
                DataStorage.Save();
                SceneManager.LoadScene(1);
                break;
            case buttonType.newGame:
                DataStorage.Reset();
                DataStorage.saveData.day = 1;
                DataStorage.Save();
                SceneManager.LoadScene(1);
                break;
        }
    }
}
