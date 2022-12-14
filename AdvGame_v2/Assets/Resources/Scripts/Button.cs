using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject buttonCombination;
    public GameObject InteractionText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SetButtonPressEnabled");
            other.GetComponentInParent<TopDownController>().buttonPressEnabled = true;
            other.GetComponentInParent<TopDownController>().buttonToPress = this.gameObject;
            InteractionText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SetButtonPressDisabled");
            other.GetComponentInParent<TopDownController>().buttonPressEnabled = false;
            other.GetComponentInParent<TopDownController>().buttonToPress = null;
            InteractionText.SetActive(false);
        }
    }

    public void PressButton()
    {
        Debug.Log("Button Pressed");
        buttonCombination.GetComponentInParent<ButtonCombination>().SubmitPress(this.gameObject);
    }
}
