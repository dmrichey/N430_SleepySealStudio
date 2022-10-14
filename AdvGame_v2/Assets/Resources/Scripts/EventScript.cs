using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    public GameObject toManipulate;

    public void TriggerEvent()
    {
        Debug.Log("Event Triggered");

        if (toManipulate.CompareTag("Door"))
        {
            toManipulate.GetComponent<Door>().OpenDoor();
        }
    }
}
