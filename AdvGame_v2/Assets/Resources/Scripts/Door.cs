using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool read = false;
    bool open = false;
    public GameObject grabText;

    public void OpenDoor()
    {
        open = true;

        this.GetComponent<PolygonCollider2D>().enabled = false;

        // Code to Move Sprite Out of the Way
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !read && !open)
        {
            Debug.Log("SetDoorAdjacent");
            other.GetComponentInParent<TopDownController>().nextToDoor = true;
            other.GetComponentInParent<TopDownController>().door = this.gameObject;
            grabText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("SetDoorNotAdjacent");
            other.GetComponentInParent<TopDownController>().nextToDoor = false;
            other.GetComponentInParent<TopDownController>().door = null;
            grabText.SetActive(false);
        }
    }

    public void SetRead()
    {
        read = true;
    }
}
