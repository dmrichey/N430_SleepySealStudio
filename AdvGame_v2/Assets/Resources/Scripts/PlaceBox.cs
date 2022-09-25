using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBox : MonoBehaviour
{
    public GameObject correctBox;
    public GameObject levelTrigger;
    public bool isExitTrigger;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KeyBox"))
        {
            if (other.gameObject == correctBox)
            {
                other.GetComponentInParent<TopDownController>().boxGrabEnabled = false;
                other.GetComponentInParent<TopDownController>().boxToMove = null;
                other.GetComponentInParent<TopDownController>().ReleaseBox();
                other.transform.parent = this.transform;
                other.transform.position = new Vector3(1.5f, 5.5f, 0);

                if (isExitTrigger)
                {
                    levelTrigger.GetComponent<LevelTrigger>().TriggerExitEvent();
                } else
                {
                    levelTrigger.GetComponent<LevelTrigger>().TriggerSideEvent();
                }

            }
        }
    }
}
