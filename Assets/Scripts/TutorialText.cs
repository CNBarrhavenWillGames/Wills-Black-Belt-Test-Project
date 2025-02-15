using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject timeBar;

    [SerializeField] private TMP_Text text;
    private int tutorialProgress = 0;
    private int tutorialProgress2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (DataStorage.saveData.day == 1)
        {
            healthBar.SetActive(false);
            timeBar.SetActive(false);
            tutorialProgress = 1;
        }

        if (DataStorage.saveData.day == 2)
        {
            tutorialProgress2 = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (tutorialProgress >= 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                tutorialProgress++;
            }

            switch (tutorialProgress)
            {
                case 1:
                    text.text = "Welcome to \"Inflitration\" (Press R to Continue)";
                    break;
                case 2:
                    text.text = "Use W, A, S, and D to Move";
                    break;
                case 3:
                    text.text = "Use Spacebar to Jump";
                    break;
                case 4:
                    text.text = "Use E to Interact with Items";
                    break;
                case 5:
                    text.text = "Use Q to Drop Items";
                    break;
                case 6:
                    text.text = "Collect the Food";
                    break;
                case 7:
                    text.text = "Exit by the Pink Door to End the Day";
                    break;
            }
        }

        if (tutorialProgress2 >= 1)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                tutorialProgress2++;
            }

            switch (tutorialProgress2)
            {
                case 1:
                    text.text = "If your Healthbar runs out, you lose";
                    break;
                case 2:
                    text.text = "If you don't exit after 4 Minutes, you lose";
                    break;
                case 3:
                    text.text = "Collect as many items as you can each day";
                    break;
                case 4:
                    text.text = "Good Luck!";
                    break;
                case 5:
                    text.text = "";
                    break;
            }
        }
    }
}
