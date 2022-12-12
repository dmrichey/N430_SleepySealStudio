using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool read = false;
    bool open = false;
    public float openTime = 2.0f;
    float timer = 0.0f;
    public GameObject grabText;
    public GameObject spriteToMove;
    public int id;

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

    void Update()
    {
        if (open && timer <= openTime)
        {
            timer += Time.deltaTime;

            Vector3 newPosition = spriteToMove.transform.position;
            newPosition.y -= 0.645f * Time.deltaTime;
            newPosition.z -= 6.0f * Time.deltaTime;
            spriteToMove.transform.position = newPosition;
        }
    }
}
