using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{

    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;

    // Animation Setup

    public float baseSpeed;
    Vector2 direction;
    float speed;

    public bool boxGrabEnabled = false;
    public bool heightTestingEnabled = false;
    public float dragSpeed;
    public int defaultSpriteLayer = 1;
    bool holdingBox = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // get direction of input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        // set walk based on direction
        body.velocity = direction * speed;

        //TESTING ONLY REMOVE LATER

        if (heightTestingEnabled && Input.GetKeyDown(KeyCode.Space)) {
            spriteRenderer.sortingOrder++;
        }

        if (boxGrabEnabled) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (!holdingBox) {
                    Debug.Log("Grab Box");
                    holdingBox = true;
                    // Attach Box to Player
                        // Find Box
                        // Parent Player to Box
                    // Change Speed
                    speed = dragSpeed;
                    // Set Drag Anim Facing Box
                } else {
                    Debug.Log("Release Box");
                    holdingBox = false;
                    // Detach Box from Player
                    // Change Speed
                    speed = baseSpeed;
                    // Resume Normal Animations
                }
            }
        }
    }
    public void ShiftLayer(int layer)
    {
        spriteRenderer.sortingOrder = layer;
    }
}
