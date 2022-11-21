using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject grabText;
    public int textID;
    public GameObject canvas;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SetItemGrabEnabled");
            other.GetComponentInParent<TopDownController>().itemGrabEnabled = true;
            other.GetComponentInParent<TopDownController>().itemToGrab = this.gameObject;
            grabText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SetItemGrabDisabled");
            other.GetComponentInParent<TopDownController>().itemGrabEnabled = false;
            other.GetComponentInParent<TopDownController>().itemToGrab = null;
            grabText.SetActive(false);
        }
    }

    public void Collect()
    {
        canvas.SetActive(true);
    }
}
