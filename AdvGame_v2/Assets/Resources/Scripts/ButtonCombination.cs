using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCombination : MonoBehaviour
{
    int numButtonsPressed = 0;
    int combinationSize;
    public GameObject[] buttonsToPress;

    public GameObject levelTrigger;
    public bool isExitTrigger;

    void Start()
    {
        combinationSize = buttonsToPress.Length;
    }

    public void SubmitPress(GameObject button)
    {
        if (button == buttonsToPress[numButtonsPressed])
        {
            Debug.Log("Correct Button");
            numButtonsPressed++;
            if (numButtonsPressed == combinationSize)
            {
                if (isExitTrigger)
                {
                    levelTrigger.GetComponent<LevelTrigger>().TriggerExitEvent();
                }
                else
                {
                    levelTrigger.GetComponent<LevelTrigger>().TriggerSideEvent();
                }
            }
        } else
        {
            Debug.Log("Incorrect Button. Resetting...");
            numButtonsPressed = 0;
        }
    }
}
