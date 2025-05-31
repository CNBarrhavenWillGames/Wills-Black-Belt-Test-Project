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

    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (DataStorage.saveData.day == 1)
        {
            healthBar.SetActive(false);
            timeBar.SetActive(false);
            tutorialProgress = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (tutorialProgress >= 1)
        {

            switch (tutorialProgress)
            {
                case 1:
                    text.text = "Use W, A, S, and D to Move";
                    if (player.transform.position.z >= -217)
                    {
                        tutorialProgress++;
                    }
                    break;
                case 2:
                    text.text = "Use Spacebar to Jump";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        tutorialProgress++;
                    }
                    break;
                case 3:
                    text.text = "Use E to Interact with Items (Q to drop)";
                    if (player.GetComponent<MovementScript>().weight >= 3)
                    {
                        tutorialProgress++;
                    }
                    break;
                case 4:
                    text.text = "Collect all of the Food";
                    if (player.GetComponent<MovementScript>().weight >= 37)
                    {
                        tutorialProgress++;
                    }
                    break;
                case 5:
                    text.text = "Flick the Lever in the corner";
                    if (DataStorage.lever == true)
                    {
                        tutorialProgress++;
                    }
                    break;
                case 6:
                    text.text = "Exit by the Red Door to End the Day";
                    if (player.transform.position.z >= -196)
                    {
                        tutorialProgress++;
                    }
                    break;
                case 7:
                    text.text = "Good Luck!";
                    break;

            }
        }
    }
}
