using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public int exitTriggerCount;
    int eTriggerCounter = 0;
    public int sideTriggerCount;
    int sTriggerCounter = 0;

    public GameObject exitEffect;
    public GameObject sideEffect;
    public GameObject sideEffect2;

    public void TriggerExitEvent()
    {
        eTriggerCounter++;
        if (eTriggerCounter == exitTriggerCount)
        {
            exitEffect.GetComponent<EventScript>().TriggerEvent();
        }
    }

    public void TriggerSideEvent()
    {
        sTriggerCounter++;
        if (sTriggerCounter == sideTriggerCount)
        {
            sideEffect.GetComponent<EventScript>().TriggerEvent();
            if (sideEffect2 != null)
            {
                sideEffect2.GetComponent<EventScript>().TriggerEvent();
            }
        }
    }
}
