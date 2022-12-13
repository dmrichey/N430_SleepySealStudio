using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    // Class needs to be attached to the player prefab

    public  Rigidbody2D rb;

    [SerializeField]
    public float strength = 17;
    public float delay = 0.15f;

    

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    public void PlayKnockBack(GameObject sender) 
    {
        
        StopAllCoroutines();
        Vector2 direction = (transform.position - sender.transform.position).normalized;

        Debug.Log(direction);

        GetComponent<TopDownController>().movementEnabled = false;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());

    }

    private IEnumerator Reset() 
    {
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector2.zero;
        GetComponent<TopDownController>().EnableMovement();

    }


}
