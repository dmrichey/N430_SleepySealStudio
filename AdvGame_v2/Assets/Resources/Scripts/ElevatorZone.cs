using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorZone : MonoBehaviour
{
    public int lowerLayer;
    public int upperLayer;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            if (other.GetComponentInParent<TopDownController>().currentSortingOrder == lowerLayer)
            {
                other.GetComponentInParent<TopDownController>().ShiftLayer(upperLayer);
            } else
            {
                other.GetComponentInParent<TopDownController>().ShiftLayer(lowerLayer);
            }            
        }
    }


}

