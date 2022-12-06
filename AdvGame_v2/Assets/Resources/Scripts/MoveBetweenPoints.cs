using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private float target_margin = .05f;

    [SerializeField]
    private GameObject[] Points;

    public int index;
    private Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Points.Length > 0) {
            //get current target 
            target = Points[index].transform;

            //Move closer to target
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, speed * Time.deltaTime);

            //Check if we are in range of the target points
            if (Vector3.Distance(target.position, gameObject.transform.position) < target_margin)
            {
                index++; 

                if (index == Points.Length)
                {
                    index = 0;
                }
                           
            }


        }
    }
}
