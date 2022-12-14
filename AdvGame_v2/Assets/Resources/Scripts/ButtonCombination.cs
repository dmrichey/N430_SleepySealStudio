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

    AudioSource audio;
    public AudioClip[] sounds;

    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        combinationSize = buttonsToPress.Length;
    }

    public void SubmitPress(GameObject button)
    {
        if (button == buttonsToPress[numButtonsPressed])
        {
            Debug.Log("Correct Button");
            audio.clip = sounds[0];
            numButtonsPressed++;
            if (numButtonsPressed == combinationSize)
            {
                audio.clip = sounds[1];
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
            audio.clip = sounds[2];
            numButtonsPressed = 0;
        }
        audio.Play();
    }
}
