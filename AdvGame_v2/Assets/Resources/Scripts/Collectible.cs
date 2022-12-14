using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public GameObject grabText;
    public int textID;
    public int entryID;
    Collider2D player;
    bool collected;
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !collected)
        {
            Debug.Log("SetItemGrabEnabled");
            player = other;
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
        collected = true;
        player.GetComponentInParent<TopDownController>().itemGrabEnabled = false;
        player.GetComponentInParent<TopDownController>().itemToGrab = null;
        grabText.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
