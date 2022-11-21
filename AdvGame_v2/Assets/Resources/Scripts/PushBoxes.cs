using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxes : MonoBehaviour
{

    public GameObject grabText;
    public GameObject releaseText;
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            Debug.Log("SetBoxGrabEnabled");
            other.GetComponentInParent<TopDownController>().boxGrabEnabled = true;
            other.GetComponentInParent<TopDownController>().boxToMove = this.gameObject;
            grabText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("SetBoxGrabDisabled");
            other.GetComponentInParent<TopDownController>().boxGrabEnabled = false;
            other.GetComponentInParent<TopDownController>().boxToMove = null;
            grabText.SetActive(false);
        }
    }
}
