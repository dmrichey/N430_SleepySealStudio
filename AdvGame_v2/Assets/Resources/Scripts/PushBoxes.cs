using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxes : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            Debug.Log("SetBoxGrabEnabled");
            other.GetComponentInParent<TopDownController>().boxGrabEnabled = true;
            other.GetComponentInParent<TopDownController>().boxToMove = this.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("SetBoxGrabDisabled");
            other.GetComponentInParent<TopDownController>().boxGrabEnabled = false;
            other.GetComponentInParent<TopDownController>().boxToMove = null;
        }
    }
}
