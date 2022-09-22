using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorZone : MonoBehaviour
{
    public int layerNum;
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<TopDownController>().ShiftLayer(layerNum);
        }
    }


}
