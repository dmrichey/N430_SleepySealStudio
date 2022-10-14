using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   public void OpenDoor()
   {
        this.GetComponent<PolygonCollider2D>().enabled = false;

        // Code to Move Sprite Out of the Way
   }
}
